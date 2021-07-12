using GeneticToolkit.Utils.DataStructs;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GeneticToolkit.Utils.TSPLIB.TSP
{
    [PublicAPI]
    public class Route
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum EDistanceMetric
        {
            Euclidean,
            Manhattan,
            Maximum,
            Geographical,
            PseudoEuclidean
        }

        private const double Pi = 3.141592;

        private List<Vector2F64> _points;

        public List<Vector2F64> Points
        {
            get => _points;
            set
            {
                _points = value;
                DefaultOrder = new short[_points.Count];
                for (short i = 0; i < _points.Count; i++)
                    DefaultOrder[i] = i;
            }
        }

        public short[] DefaultOrder { get; set; }

        private static Tuple<double, double> ToGeographical(Vector2F64 v)
        {
            double deg = (int) (v.X + 0.5);
            double min = v.X - deg;
            double latitude = Pi * (deg + 5.0 * min / 3.0) / 180;

            deg = (int) (v.Y + 0.5);
            min = v.Y - deg;
            double longitude = Pi * (deg + 5.0 * min / 3.0) / 180;
            return new Tuple<double, double>(latitude, longitude);
        }

        public double GetLength(EDistanceMetric distanceMetric = EDistanceMetric.Euclidean)
        {
            return GetLength(DefaultOrder, distanceMetric);
        }
        public double GetLength(short[] order, EDistanceMetric distanceMetric = EDistanceMetric.Euclidean)
        {
            double distance = 0;
            for (var i = 0; i < Points.Count - 1; i++)
                distance += GetDistance(distanceMetric, Points[order[i]], Points[order[i + 1]]);
            distance += GetDistance(distanceMetric, Points[order[order.Length - 1]], Points[order[0]]);
            return distance;
        }

        protected static double GetDistance(EDistanceMetric distanceMetric, Vector2F64 p0, Vector2F64 p1)
        {
            double yd;
            double xd;
            switch (distanceMetric)
            {
                case EDistanceMetric.Euclidean:
                    xd = p0.X - p1.X;
                    yd = p0.Y - p1.Y;
                    return (int) (Math.Sqrt(xd * xd + yd * yd) + 0.5);
                case EDistanceMetric.Maximum:
                    xd = Math.Abs(p0.X - p1.X);
                    yd = Math.Abs(p0.Y - p1.Y);
                    return (int) (Math.Max(xd, yd) + 0.5);
                case EDistanceMetric.Manhattan:
                    xd = Math.Abs(p0.X - p1.X);
                    yd = Math.Abs(p0.Y - p1.Y);
                    return (int) (xd + yd + 0.5);
                case EDistanceMetric.Geographical:
                    const double rrr = 6378.388;
                    (double latitude0, double longitude0) = ToGeographical(p0);
                    (double latitude1, double longitude1) = ToGeographical(p1);
                    double q1 = Math.Cos(longitude0 - longitude1);
                    double q2 = Math.Cos(latitude0 - latitude1);
                    double q3 = Math.Cos(latitude0 + latitude1);
                    return (int) (rrr * Math.Acos(0.5 * ((1.0 + q1) * q2 - (1.0 - q1) * q3)) + 1.0);
                case EDistanceMetric.PseudoEuclidean:
                    xd = p0.X - p1.X;
                    yd = p0.Y - p1.Y;
                    double rij = Math.Sqrt((xd * xd + yd * yd) / 10.0);
                    var tij = (int) (rij + 0.5);
                    return tij < rij ? tij + 1 : tij;
                default:
                    return 0;
            }
        }

        
    }
}