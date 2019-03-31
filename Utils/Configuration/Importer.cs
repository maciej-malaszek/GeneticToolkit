using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            string genericParameters = "";

            if (parameter.GenericArguments.Length <= 0) 
                return $"{parameter.Type}{genericParameters}";

            List<string> types = parameter.GenericArguments
                .Select(type => Type.GetType(type) != null ? Type.GetType(type).AssemblyQualifiedName : type)
                .Select(typeName => $"[{typeName}]")
                .ToList();
            genericParameters = $"[{string.Join(",", types)}]";

            return $"{parameter.Type}{genericParameters}";
        }

        public static Type GetTypeFrom(GeneticAlgorithmParameter parameter, Type baseType)
        {
            string typeString = GetTypeString(parameter);
            Assembly assembly = baseType.Assembly;
            return assembly.GetType(typeString);
        }

        private static ISelectionMethod SelectionMethod(GeneticAlgorithmParameter parameter, ICompareCriteria compareCriteria)
        {
            Type type = GetTypeFrom(parameter, typeof(IndividualFactoryBase));
            var selectionMethod = Activator.CreateInstance(type, args: parameter.Params) as ISelectionMethod;
            if(selectionMethod != null)
                selectionMethod.CompareCriteria = compareCriteria;
            return selectionMethod;
        }

        private static ICrossover Crossover(GeneticAlgorithmParameter parameter)
        {
            Type type = GetTypeFrom(parameter, typeof(ICrossover));
            var crossover = Activator.CreateInstance(type, args: parameter.Params) as ICrossover;
            return crossover;
        }

        private static IHeavenPolicy HeavenPolicy(GeneticAlgorithmParameter parameter)
        {
            Type type = GetTypeFrom(parameter, typeof(IHeavenPolicy));
            var heavenPolicy = Activator.CreateInstance(type, args: parameter.Params) as IHeavenPolicy;
            return heavenPolicy;
        }

        private static IIncompatibilityPolicy IncompatibilityPolicy(GeneticAlgorithmParameter parameter, Func<IPopulation, IIndividual, bool> isCompatibleFunc)
        {
            Type type = GetTypeFrom(parameter, typeof(IIncompatibilityPolicy));
            var incompatibilityPolicy = Activator.CreateInstance(type, args: parameter.Params) as IIncompatibilityPolicy;
            if(incompatibilityPolicy != null)
                incompatibilityPolicy.IsCompatible = isCompatibleFunc;
            return incompatibilityPolicy;
        }

        private static IPopulationResizePolicy ResizePolicy(GeneticAlgorithmParameter parameter)
        {
            Type type = GetTypeFrom(parameter, typeof(IPopulationResizePolicy));
            var resizePolicy = Activator.CreateInstance(type, args: parameter.Params) as IPopulationResizePolicy;
            return resizePolicy;
        }

        private static IndividualFactoryBase IndividualFactory(GeneticAlgorithmParameter parameter)
        {
            Type type = GetTypeFrom(parameter, typeof(IndividualFactoryBase));
            JObject phenotypeFactoryJObject = (JObject) parameter.Params["PhenotypeFactory"];
            GeneticAlgorithmParameter phenotypeFactoryParameter =
                phenotypeFactoryJObject.ToObject<GeneticAlgorithmParameter>();

            IPhenotypeFactory<IPhenotype> phenotypeFactory = PhenotypeFactory(phenotypeFactoryParameter);
            parameter.Params["PhenotypeFactory"] = phenotypeFactory;
            
            return Activator.CreateInstance(type, args: parameter.Params) as IndividualFactoryBase;
        }

        private static IPhenotypeFactory<IPhenotype> PhenotypeFactory(GeneticAlgorithmParameter parameter)
        {
            Type type = GetTypeFrom(parameter, typeof(IPhenotypeFactory<IPhenotype>));
            var phenotypeFactory = Activator.CreateInstance(type, args: parameter.Params) as IPhenotypeFactory<IPhenotype>;
            return phenotypeFactory;
        }

        public static IPopulation GetPopulation(GeneticAlgorithmSettings settings, IFitnessFunction function)
        {
            Type type = GetTypeFrom(new GeneticAlgorithmParameter(){Type = settings.Type, GenericArguments = settings.GenericArguments}, typeof(IPopulation));
            var population = Activator.CreateInstance(type, args: new object[]{function, settings.CustomParameters}) as IPopulation;
            return population;
        }
    }
}
