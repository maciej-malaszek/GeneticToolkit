using System;
using System.Linq;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;
using GeneticToolkit.Utils.Factories;
using Newtonsoft.Json.Linq;

namespace GeneticToolkit.Utils.Configuration
{
    public static class Importer
    {
        public static string GetTypeString(GeneticAlgorithmParameter parameter)
        {
            var genericParameters = "";

            if (parameter.GenericArguments.Length <= 0)
                return $"{parameter.Type}{genericParameters}";

            var types = parameter.GenericArguments
                .Select(type => Type.GetType(type) != null ? Type.GetType(type).AssemblyQualifiedName : type)
                .Select(typeName => $"[{typeName}]")
                .ToList();
            genericParameters = $"[{string.Join(",", types)}]";

            return $"{parameter.Type}{genericParameters}";
        }

        public static Type GetTypeFrom(GeneticAlgorithmParameter parameter, Type baseType)
        {
            var typeString = GetTypeString(parameter);
            var assembly = baseType.Assembly;
            return assembly.GetType(typeString);
        }

        private static ISelectionMethod SelectionMethod(GeneticAlgorithmParameter parameter,
            ICompareCriteria compareCriteria)
        {
            var type = GetTypeFrom(parameter, typeof(IndividualFactoryBase));
            var selectionMethod = Activator.CreateInstance(type, args: parameter.Params) as ISelectionMethod;
            if (selectionMethod != null)
                selectionMethod.CompareCriteria = compareCriteria;
            return selectionMethod;
        }

        private static ICrossover Crossover(GeneticAlgorithmParameter parameter)
        {
            var type = GetTypeFrom(parameter, typeof(ICrossover));
            var crossover = Activator.CreateInstance(type, args: parameter.Params) as ICrossover;
            return crossover;
        }

        private static IHeavenPolicy HeavenPolicy(GeneticAlgorithmParameter parameter)
        {
            var type = GetTypeFrom(parameter, typeof(IHeavenPolicy));
            var heavenPolicy = Activator.CreateInstance(type, args: parameter.Params) as IHeavenPolicy;
            return heavenPolicy;
        }

        private static IIncompatibilityPolicy IncompatibilityPolicy(GeneticAlgorithmParameter parameter,
            Func<IPopulation, IIndividual, bool> isCompatibleFunc)
        {
            var type = GetTypeFrom(parameter, typeof(IIncompatibilityPolicy));
            var incompatibilityPolicy =
                Activator.CreateInstance(type, args: parameter.Params) as IIncompatibilityPolicy;
            if (isCompatibleFunc != null && incompatibilityPolicy != null && incompatibilityPolicy.IsCompatible == null)
                incompatibilityPolicy.IsCompatible = isCompatibleFunc;
            return incompatibilityPolicy;
        }

        private static IPopulationResizePolicy ResizePolicy(GeneticAlgorithmParameter parameter)
        {
            var type = GetTypeFrom(parameter, typeof(IPopulationResizePolicy));
            var resizePolicy = Activator.CreateInstance(type, args: parameter.Params) as IPopulationResizePolicy;
            return resizePolicy;
        }

        private static IndividualFactoryBase IndividualFactory(GeneticAlgorithmParameter parameter)
        {
            var type = GetTypeFrom(parameter, typeof(IndividualFactoryBase));
            var phenotypeFactoryJObject = (JObject) parameter.Params["PhenotypeFactory"];
            var phenotypeFactoryParameter =
                phenotypeFactoryJObject.ToObject<GeneticAlgorithmParameter>();

            var phenotypeFactory = PhenotypeFactory(phenotypeFactoryParameter);
            parameter.Params["PhenotypeFactory"] = phenotypeFactory;

            return Activator.CreateInstance(type, args: parameter.Params) as IndividualFactoryBase;
        }

        private static IPhenotypeFactory<IPhenotype> PhenotypeFactory(GeneticAlgorithmParameter parameter)
        {
            var type = GetTypeFrom(parameter, typeof(IPhenotypeFactory<IPhenotype>));
            var phenotypeFactory =
                Activator.CreateInstance(type, args: parameter.Params) as IPhenotypeFactory<IPhenotype>;
            return phenotypeFactory;
        }

        public static IPopulation GetPopulation(GeneticAlgorithmSettings settings, IFitnessFunction function,
            ICompareCriteria compareCriteria, Func<IPopulation, IIndividual, bool> isCompatibleFunc)
        {
            var type = GetTypeFrom(
                new GeneticAlgorithmParameter {Type = settings.Type, GenericArguments = settings.GenericArguments},
                typeof(IPopulation));
            var population = Activator.CreateInstance(type, function, settings.CustomParameters) as IPopulation;
            if (population == null)
                return null;

            population.CompareCriteria = compareCriteria;
            population.SelectionMethod = SelectionMethod(settings.SelectionMethod, compareCriteria);
            population.Crossover = Crossover(settings.Crossover);
            population.HeavenPolicy = HeavenPolicy(settings.HeavenPolicy);
            population.IncompatibilityPolicy =
                IncompatibilityPolicy(settings.IncompatibilityPolicy, isCompatibleFunc);
            population.ResizePolicy = ResizePolicy(settings.ResizePolicy);
            population.IndividualFactory = IndividualFactory(settings.IndividualFactory);

            return population;
        }
    }
}