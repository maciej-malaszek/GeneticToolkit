using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeneticToolkit.Utils.Configuration
{
    public class GeneticAlgorithmConfiguration
    {
    }

    public class DynamicObjectInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Value { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<DynamicObjectInfo> Properties { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<DynamicObjectInfo> Parameters { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<string> GenericParameters { get; set; }

        private Dictionary<string, string> _typeAssembly = new();

        private string GetAssemblyName(string type)
        {
            if (_typeAssembly.ContainsKey(type))
            {
                return _typeAssembly[type];
            }

            var assembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(s => s.FullName != null)
                .Where(s => type.StartsWith(s.FullName.Split(",").First()))
                .FirstOrDefault(s => 
                    s.GetTypes().Select(t => t.FullName).Contains(type)
                );
            _typeAssembly[type] = assembly?.FullName;
            return _typeAssembly[type];
        }

        public bool IsGenericType()
        {
            return (GenericParameters?.Count ?? 0) > 0;
        }

        public bool IsArray()
        {
            return Type.EndsWith("[]");
        }

        public bool IsEnum()
        {
            return BuildType().IsEnum;
        }

        public string ArrayTypeName()
        {
            if (IsArray() == false)
            {
                return null;
            }

            var typeName = Type.Remove(Type.IndexOf("[]", StringComparison.Ordinal));
            return IsGenericType() ? $"{typeName}`{GenericParameters.Count}" : typeName;
        }

        public string BuildStringType()
        {
            if (IsArray())
            {
                return IsGenericType() ? $"{ArrayTypeName()}`{GenericParameters.Count}[]" : Type;
            }

            return IsGenericType() ? $"{Type}`{GenericParameters.Count}" : $"{Type}";
        }

        public Type BuildType()
        {
            var type = System.Type.GetType(IsArray() ? $"{ArrayTypeName()}" : BuildStringType());
            if (!IsGenericType())
            {
                return type;
            }

            if (type == null)
            {
                return null;
            }

            var genericParameters = new Type[GenericParameters.Count];


            for (var i = 0; i < genericParameters.Length; i++)
            {
                var assemblyName = GetAssemblyName(GenericParameters[i]);
                
                var assemblyTypeName = string.IsNullOrWhiteSpace(assemblyName) 
                    ? GenericParameters[i] 
                    : $"{GenericParameters[i]},{assemblyName}";
                genericParameters[i] = System.Type.GetType(assemblyTypeName);
            }

            var genericType = type.MakeGenericType(genericParameters);
            return genericType;
            //  return IsArray() ? Array.CreateInstance(genericType, 1).GetType() : genericType;
        }
    }

    public static class DynamicObjectFactory<T>
    {
        public static ConstructorInfo FindConstructor(DynamicObjectInfo objectInfo)
        {
            var type = objectInfo.BuildType();
            var constructors = type.GetConstructors();
            var constructor = constructors
                .FirstOrDefault(c => c.GetParameters()
                    .Select(p => p.ParameterType.FullName)
                    .OrderBy(p => p)
                    .SequenceEqual(objectInfo.Parameters.Select(o => o.Type).OrderBy(p => p)));
            return constructor;
        }

        public static dynamic[] GetConstructorParameters(DynamicObjectInfo objectInfo, ConstructorInfo constructor)
        {
            var parameterValues =
                objectInfo.Parameters.ToDictionary(
                    p => p.Name.ToLower(),
                    p => p.Value is IConvertible ? Convert.ChangeType(p.Value, p.BuildType()) : p.Value
                );
            var parameters = constructor?
                .GetParameters()
                .OrderBy(p => p.Position)
                .Select(p => parameterValues[p.Name!.ToLower()])
                .ToArray();
            return parameters;
        }

        public static T CreateInstance(DynamicObjectInfo objectInfo, Type type)
        {
            if ((objectInfo.Parameters?.Count ?? 0) == 0)
            {
                return (T) Activator.CreateInstance(type);
            }

            var constructor = FindConstructor(objectInfo);
            if (constructor == null)
            {
                return default;
            }

            var parameters = GetConstructorParameters(objectInfo, constructor);
            return (T) constructor.Invoke(parameters);
        }

        public static dynamic ParsePrimitiveValue(DynamicObjectInfo objectInfo)
        {
            var type = objectInfo.BuildType();
            if (type == null)
            {
                return default(T);
            }

            if (type.IsArray)
            {
                var value = (JArray) objectInfo.Value;
                var values = value.ToObject<List<DynamicObjectInfo>>()?
                    .Select(DynamicObjectFactory<dynamic>.Build)
                    .ToArray();
                return values;
            }

            if (type.IsEnum)
            {
                return Enum.Parse(type, objectInfo.Value);
            }

            if (type.IsPrimitive || type == typeof(string))
            {
                return objectInfo.Value;
            }

            return default(T);
        }

        public static void SetPropertyValue(DynamicObjectInfo propertyInfo, dynamic instance)
        {
            var value = DynamicObjectFactory<dynamic>.Build(propertyInfo);
            var type = instance.GetType();
            if (type.IsEnum)
            {
                type.GetProperty(propertyInfo.Name)?.SetValue(instance, Enum.Parse(type, value));
            }
            else if (value is IConvertible)
            {
                type.GetProperty(propertyInfo.Name)?.SetValue(instance, Convert.ChangeType(value, propertyInfo.BuildType()));
            }
            else
            {
                type.GetProperty(propertyInfo.Name)?.SetValue(instance, value);
            }
        }

        public static void SetArrayPropertyValue(DynamicObjectInfo propertyInfo, dynamic instance)
        {
            var infos = ((JArray) propertyInfo.Value).Select(t => t.ToObject<DynamicObjectInfo>()).ToList();
            var values = infos.Select(DynamicObjectFactory<dynamic>.Build).ToArray();
            var arrayType = propertyInfo.BuildType();
            if (arrayType == null)
            {
                return;
            }
            var arrayInstance = Array.CreateInstance(arrayType, values.Length);
            Array.Copy(values, arrayInstance, values.Length);
            instance.GetType().GetProperty(propertyInfo.Name)?.SetValue(instance, arrayInstance);
        }

        public static T Build(DynamicObjectInfo objectInfo)
        {
            T instance = ParsePrimitiveValue(objectInfo);
            if (instance != null && !instance.Equals(default(T)))
            {
                return instance;
            }

            var type = objectInfo.BuildType();
            instance = CreateInstance(objectInfo, type);

            if (objectInfo.Properties == null)
            {
                return instance;
            }

            foreach (var property in objectInfo.Properties)
            {
                if (property == null)
                {
                    continue;
                }

                if (property.IsArray())
                {
                    SetArrayPropertyValue(property, instance);
                }
                else
                {
                    SetPropertyValue(property, instance);
                }
            }

            return instance;
        }

        public static DynamicObjectInfo Serialize(object instance, string name)
        {
            if (instance == null)
            {
                return null;
            }

            var type = instance.GetType();
            if (type.IsAssignableTo(typeof(MulticastDelegate)))
            {
                return null;
            }

            if (type.IsPrimitive || type == typeof(string) || type.IsEnum || type == typeof(DateTime) || type == typeof(Guid))
            {
                return new DynamicObjectInfo
                {
                    Name = name,
                    Properties = null,
                    GenericParameters = null,
                    Type = type.FullName,
                    Value = instance
                };
            }

            if (instance is TimeSpan)
            {
                return new DynamicObjectInfo()
                {
                    Name = name,
                    Parameters = new List<DynamicObjectInfo>
                    {
                        new() {Name = "ticks", Type = "System.Int64", Value = ((TimeSpan) instance).Ticks}
                    },
                    Type = "System.TimeSpan"
                };
            }

            if (type.IsArray)
            {
                type = type.UnderlyingSystemType;
                var typeName = type.GetGenericArguments().ToList().Count > 0
                    ? type.FullName.Remove(type.FullName.IndexOf('`'))
                    : type.FullName;
                if (!typeName.EndsWith("[]"))
                {
                    typeName += "[]";
                }

                return new DynamicObjectInfo
                {
                    Name = name,
                    Properties = null,
                    GenericParameters = type.GetGenericArguments().Select(t => t.FullName).ToList(),
                    Type = typeName,
                    Value = ((System.Collections.IEnumerable) instance)?.Cast<object>().Select(obj => Serialize(obj, name)).ToArray()
                };
            }

            var info = new DynamicObjectInfo
            {
                Name = name,
                Properties = new List<DynamicObjectInfo>(),
                GenericParameters = type.GetGenericArguments().Select(a => a.FullName).ToList()
            };
            info.Type = info.GenericParameters?.Count > 0 ? type.FullName.Remove(type.FullName.IndexOf('`')) : type.FullName;

            var properties = type.GetProperties().Where(p => p.CanWrite && p.CanRead && p.GetIndexParameters().Length == 0);
            foreach (var property in properties)
            {
                var serialized = Serialize(property.GetValue(instance), property.Name);
                info.Properties.Add(serialized);
            }

            return info;
        }
    }
}