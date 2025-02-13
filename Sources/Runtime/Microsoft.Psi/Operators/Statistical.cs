﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods that simplify operator usage
    /// </summary>
    public static partial class Operators
    {
#region `Count`, `LongCount`

        /// <summary>
        /// Returns a stream of ints representing the number of elements in a stream satisfying a condition.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each element for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of counts.</returns>
        public static IProducer<int> Count<T>(this IProducer<T> source, Predicate<T> condition, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Where(condition, deliveryPolicy).Count(DeliveryPolicy.Unlimited);
            }
        }

        /// <summary>
        /// Returns a stream of int values representing the number of elements in a stream.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of counts.</returns>
        public static IProducer<int> Count<T>(this IProducer<T> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Aggregate(0, (count, _) => count + 1, deliveryPolicy);
        }

        /// <summary>
        /// Returns a stream of long values representing the number of elements in a stream satisfying a condition.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each element for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of counts.</returns>
        public static IProducer<long> LongCount<T>(this IProducer<T> source, Predicate<T> condition, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Where(condition, deliveryPolicy).LongCount(DeliveryPolicy.Unlimited);
            }
        }

        /// <summary>
        /// Returns a stream of long values representing the number of elements in a stream.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of counts.</returns>
        public static IProducer<long> LongCount<T>(this IProducer<T> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Aggregate(0L, (count, _) => count + 1, deliveryPolicy);
        }

#endregion // `Count`, `LongCount`
#region `Sum`

        /// <summary>
        /// Compute the sum of a stream of int values satisfying a condition.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each value for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<int> Sum(this IProducer<int> source, Predicate<int> condition, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Where(condition, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum of a stream of int values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<int> Sum(this IProducer<int> source, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Aggregate(0, (sum, message) => sum + message, deliveryPolicy);
            }
        }

        /// <summary>
        /// Compute the sum of a stream of nullable int values satisfying a condition.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each value for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<int?> Sum(this IProducer<int?> source, Predicate<int?> condition, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Where(condition, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable int values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<int?> Sum(this IProducer<int?> source, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Aggregate((int?)0, (sum, message) => sum + message.GetValueOrDefault(), deliveryPolicy);
            }
        }

        /// <summary>
        /// Compute the sum of a stream of long values satisfying a condition.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each value for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<long> Sum(this IProducer<long> source, Predicate<long> condition, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Where(condition, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum of a stream of long values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<long> Sum(this IProducer<long> source, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Aggregate(0L, (sum, message) => sum + message, deliveryPolicy);
            }
        }

        /// <summary>
        /// Compute the sum of a stream of nullable long values satisfying a condition.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each value for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<long?> Sum(this IProducer<long?> source, Predicate<long?> condition, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Where(condition, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable long values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<long?> Sum(this IProducer<long?> source, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Aggregate((long?)0, (sum, message) => sum + message.GetValueOrDefault(), deliveryPolicy);
            }
        }

        /// <summary>
        /// Compute the sum of a stream of float values satisfying a condition.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each value for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<float> Sum(this IProducer<float> source, Predicate<float> condition, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Where(condition, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum of a stream of float values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        /// <remarks>
        /// This operator considers the sum of a number and NaN as NaN. Consequently, once a value
        /// of NaN is encountered on the source stream, the corresponding output value and all
        /// subsequent output values will be NaN.
        /// </remarks>
        public static IProducer<float> Sum(this IProducer<float> source, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Aggregate(0f, (sum, message) => sum + message, deliveryPolicy);
            }
        }

        /// <summary>
        /// Compute the sum of a stream of nullable float values satisfying a condition.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each value for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<float?> Sum(this IProducer<float?> source, Predicate<float?> condition, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Where(condition, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable float values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<float?> Sum(this IProducer<float?> source, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Aggregate((float?)0, (sum, message) => sum + message.GetValueOrDefault(), deliveryPolicy);
            }
        }

        /// <summary>
        /// Compute the sum of a stream of double values satisfying a condition.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each value for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<double> Sum(this IProducer<double> source, Predicate<double> condition, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Where(condition, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum of a stream of double values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        /// <remarks>
        /// This operator considers the sum of a number and NaN as NaN. Consequently, once a value
        /// of NaN is encountered on the source stream, the corresponding output value and all
        /// subsequent output values will be NaN.
        /// </remarks>
        public static IProducer<double> Sum(this IProducer<double> source, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Aggregate(0d, (sum, message) => sum + message, deliveryPolicy);
            }
        }

        /// <summary>
        /// Compute the sum of a stream of nullable double values satisfying a condition.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each value for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<double?> Sum(this IProducer<double?> source, Predicate<double?> condition, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Where(condition, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable double values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<double?> Sum(this IProducer<double?> source, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Aggregate((double?)0, (sum, message) => sum + message.GetValueOrDefault(), deliveryPolicy);
            }
        }

        /// <summary>
        /// Compute the sum of a stream of decimal values satisfying a condition.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each value for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<decimal> Sum(this IProducer<decimal> source, Predicate<decimal> condition, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Where(condition, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum of a stream of decimal values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<decimal> Sum(this IProducer<decimal> source, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Aggregate(0m, (sum, message) => sum + message, deliveryPolicy);
            }
        }

        /// <summary>
        /// Compute the sum of a stream of nullable decimal values satisfying a condition.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">A function to test each value for a condition.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<decimal?> Sum(this IProducer<decimal?> source, Predicate<decimal?> condition, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Where(condition, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable decimal values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sums.</returns>
        public static IProducer<decimal?> Sum(this IProducer<decimal?> source, DeliveryPolicy deliveryPolicy = null)
        {
            checked
            {
                return source.Aggregate((decimal?)0, (sum, message) => sum + message.GetValueOrDefault(), deliveryPolicy);
            }
        }

#endregion // `Sum`
#region `Min`/`Max`

        /// <summary>
        /// Compute the minimum of a stream of numeric values.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="source">The source stream</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum values.</returns>
        public static IProducer<T> Min<T>(this IProducer<T> source, DeliveryPolicy deliveryPolicy = null)
        {
            var comparer = Comparer<T>.Default;
            return source.Aggregate((x, y) => comparer.Compare(x, y) < 0 ? x : y, deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum of a stream of double values.
        /// </summary>
        /// <param name="source">The source stream</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum values.</returns>
        /// <remarks>
        /// This operator considers the minimum of a number and NaN to be NaN. Consequently, once a
        /// value of NaN is encountered on the source stream, the corresponding output value and all
        /// subsequent output values will be NaN.
        /// </remarks>
        public static IProducer<double> Min(this IProducer<double> source, DeliveryPolicy deliveryPolicy = null)
        {
            // special case commonly used `double` for performance
            return source.Aggregate(Math.Min, deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum of a stream of float values.
        /// </summary>
        /// <param name="source">The source stream</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum values.</returns>
        /// <remarks>
        /// This operator considers the minimum of a number and NaN to be NaN. Consequently, once a
        /// value of NaN is encountered on the source stream, the corresponding output value and all
        /// subsequent output values will be NaN.
        /// </remarks>
        public static IProducer<float> Min(this IProducer<float> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Aggregate(Math.Min, deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum of a stream of numeric values.
        /// </summary>
        /// <typeparam name="T">The message type.</typeparam>
        /// <param name="source">The source stream</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum values.</returns>
        public static IProducer<T> Max<T>(this IProducer<T> source, DeliveryPolicy deliveryPolicy = null)
        {
            var comparer = Comparer<T>.Default;
            return source.Aggregate((x, y) => comparer.Compare(x, y) > 0 ? x : y, deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum of a stream of double values.
        /// </summary>
        /// <param name="source">The source stream</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum values.</returns>
        /// <remarks>
        /// This operator considers the maximum of a number and NaN to be NaN. Consequently, once a
        /// value of NaN is encountered on the source stream, the corresponding output value and all
        /// subsequent output values will be NaN.
        /// </remarks>
        public static IProducer<double> Max(this IProducer<double> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Aggregate(Math.Max, deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum of a stream of float values.
        /// </summary>
        /// <param name="source">The source stream</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum values.</returns>
        /// <remarks>
        /// This operator considers the maximum of a number and NaN to be NaN. Consequently, once a
        /// value of NaN is encountered on the source stream, the corresponding output value and all
        /// subsequent output values will be NaN.
        /// </remarks>
        public static IProducer<float> Max(this IProducer<float> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Aggregate(Math.Max, deliveryPolicy);
        }

        #endregion `Min`/`Max`
        #region `Average`

        /// <summary>
        /// Compute the average of a stream of decimal values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average values.</returns>
        public static IProducer<decimal> Average(this IProducer<decimal> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Aggregate(
                ValueTuple.Create((long)0, (decimal)0),
                (tuple, message) =>
                    tuple.Item1 == 0 ?
                        ValueTuple.Create((long)1, message) :
                        ValueTuple.Create(tuple.Item1 + 1, ((tuple.Item2 * tuple.Item1) + message) / (tuple.Item1 + 1)), deliveryPolicy).Select(tuple => tuple.Item2, DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average of a stream of double values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average values.</returns>
        /// <remarks>
        /// This operator considers the average of a sequence of values containing NaN to be NaN.
        /// Consequently, once a value of NaN is encountered on the source stream, the corresponding
        /// output value and all subsequent output values will be NaN.
        /// </remarks>
        public static IProducer<double> Average(this IProducer<double> source, DeliveryPolicy deliveryPolicy = null)
        {
            // keeping (most common) double-specific implementation for performance
            return source.Aggregate(
                ValueTuple.Create((long)0, (double)0),
                (tuple, message) =>
                    tuple.Item1 == 0 ?
                        ValueTuple.Create((long)1, message) :
                        ValueTuple.Create(tuple.Item1 + 1, ((tuple.Item2 * tuple.Item1) + message) / (tuple.Item1 + 1)), deliveryPolicy).Select(tuple => tuple.Item2, DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average of a stream of float values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average values.</returns>
        /// <remarks>
        /// This operator considers the average of a sequence of values containing NaN to be NaN.
        /// Consequently, once a value of NaN is encountered on the source stream, the corresponding
        /// output value and all subsequent output values will be NaN.
        /// </remarks>
        public static IProducer<float> Average(this IProducer<float> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(f => (double)f, deliveryPolicy).Average(DeliveryPolicy.Unlimited).Select(d => (float)d, DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average of a stream of int values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<int> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(i => (double)i, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average of a stream of long values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<long> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(i => (double)i, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        #endregion `Average`
        #region `Std`

        /// <summary>
        /// Compute standard deviation of (double) values.
        /// </summary>
        /// <param name="source">Source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of standard deviation (double) values.</returns>
        /// <remarks>
        /// This operator considers the standard deviation of a sequence of values containing NaN
        /// to be NaN. Consequently, once a value of NaN is encountered on the source stream, the
        /// corresponding output value and all subsequent output values will be NaN.
        /// </remarks>
        public static IProducer<double> Std(this IProducer<double> source, DeliveryPolicy deliveryPolicy = null)
        {
            long count = 0;
            double mean = 0, q = 0;

            // see: https://en.wikipedia.org/wiki/Standard_deviation#Rapid_calculation_methods
            return source.Select(
                value =>
                {
                    count++;
                    var oldmean = mean;
                    mean = mean + ((value - mean) / count);
                    q = q + ((value - oldmean) * (value - mean));
                    return Math.Sqrt(q / (count - 1));
                }, deliveryPolicy);
        }

        /// <summary>
        /// Compute standard deviation of (double) values.
        /// </summary>
        /// <param name="source">Source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of standard deviation (double) values.</returns>
        /// <remarks>
        /// This operator considers the standard deviation of a sequence of values containing NaN
        /// to be NaN. Consequently, once a value of NaN is encountered on the source stream, the
        /// corresponding output value and all subsequent output values will be NaN.
        /// </remarks>
        public static IProducer<float> Std(this IProducer<float> source, DeliveryPolicy deliveryPolicy = null)
        {
            long count = 0;
            double mean = 0, q = 0;

            // see: https://en.wikipedia.org/wiki/Standard_deviation#Rapid_calculation_methods
            return source.Select(
                value =>
                {
                    count++;
                    var oldmean = mean;
                    mean = mean + ((value - mean) / count);
                    q = q + ((value - oldmean) * (value - mean));
                    return (float)Math.Sqrt(q / (count - 1));
                }, deliveryPolicy);
        }

        /// <summary>
        /// Compute standard deviation of (double) values.
        /// </summary>
        /// <param name="source">Source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of standard deviation (double) values.</returns>
        public static IProducer<decimal> Std(this IProducer<decimal> source, DeliveryPolicy deliveryPolicy = null)
        {
            long count = 0;
            decimal mean = 0, q = 0;

            // see: https://en.wikipedia.org/wiki/Standard_deviation#Rapid_calculation_methods
            return source.Select(
                value =>
                {
                    count++;
                    var oldmean = mean;
                    mean = mean + ((value - mean) / count);
                    q = q + ((value - oldmean) * (value - mean));
                    return (decimal)Math.Sqrt((double)q / (count - 1));
                }, deliveryPolicy);
        }

#endregion
#region LINQ

        /// <summary>
        /// Compute the sum of a stream of int values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum values.</returns>
        public static IProducer<int> Sum(this IProducer<IEnumerable<int>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable int values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum values.</returns>
        public static IProducer<int?> Sum(this IProducer<IEnumerable<int?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of long values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum values.</returns>
        public static IProducer<long> Sum(this IProducer<IEnumerable<long>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable long values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum values.</returns>
        public static IProducer<long?> Sum(this IProducer<IEnumerable<long?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of float values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum values.</returns>
        public static IProducer<float> Sum(this IProducer<IEnumerable<float>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable float values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum values.</returns>
        public static IProducer<float?> Sum(this IProducer<IEnumerable<float?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of double values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum values.</returns>
        public static IProducer<double> Sum(this IProducer<IEnumerable<double>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable double values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum values.</returns>
        public static IProducer<double?> Sum(this IProducer<IEnumerable<double?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of decimal values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum values.</returns>
        public static IProducer<decimal> Sum(this IProducer<IEnumerable<decimal>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable decimal values.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum values.</returns>
        public static IProducer<decimal?> Sum(this IProducer<IEnumerable<decimal?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of values obtained by invoking a transform function.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (int) values.</returns>
        public static IProducer<int> Sum<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, int> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of int values obtained by invoking a transform function.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable int) values.</returns>
        public static IProducer<int?> Sum<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, int?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of int values obtained by invoking a transform function.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (long) values.</returns>
        public static IProducer<long> Sum<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, long> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of int values obtained by invoking a transform function.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable long) values.</returns>
        public static IProducer<long?> Sum<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, long?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of int values obtained by invoking a transform function.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (float) values.</returns>
        public static IProducer<float> Sum<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, float> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of int values obtained by invoking a transform function.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable float) values.</returns>
        public static IProducer<float?> Sum<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, float?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of decimal values.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (double) values.</returns>
        public static IProducer<double> Sum<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, double> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable decimal values.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable double) values.</returns>
        public static IProducer<double?> Sum<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, double?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of decimal values.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (decimal) values.</returns>
        public static IProducer<decimal> Sum<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, decimal> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the sum of a stream of nullable decimal values.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable decimal) values.</returns>
        public static IProducer<decimal?> Sum<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, decimal?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Sum(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<int> Min(this IProducer<IEnumerable<int>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<int?> Min(this IProducer<IEnumerable<int?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<long> Min(this IProducer<IEnumerable<long>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<long?> Min(this IProducer<IEnumerable<long?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<float> Min(this IProducer<IEnumerable<float>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Min(xs), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<float?> Min(this IProducer<IEnumerable<float?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Min(xs), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<double> Min(this IProducer<IEnumerable<double>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Min(xs), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<double?> Min(this IProducer<IEnumerable<double?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Min(xs), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<decimal> Min(this IProducer<IEnumerable<decimal>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<decimal?> Min(this IProducer<IEnumerable<decimal?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the minimum int within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<TSource> Min<TSource>(this IProducer<IEnumerable<TSource>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum int within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<int> Min<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, int> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum nullable int within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable int) values.</returns>
        public static IProducer<int?> Min<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, int?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum long within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (long) values.</returns>
        public static IProducer<long> Min<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, long> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum nullable long within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable long) values.</returns>
        public static IProducer<long?> Min<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, long?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum float within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (float) values.</returns>
        public static IProducer<float> Min<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, float> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Min(xs.Select(selector)), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum nullable float within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable float) values.</returns>
        public static IProducer<float?> Min<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, float?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Min(xs.Select(selector)), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum double within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (double) values.</returns>
        public static IProducer<double> Min<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, double> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Min(xs.Select(selector)), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum nullable double within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable double) values.</returns>
        public static IProducer<double?> Min<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, double?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Min(xs.Select(selector)), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum decimal within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (decimal) values.</returns>
        public static IProducer<decimal> Min<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, decimal> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum nullable decimal within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable decimal) values.</returns>
        public static IProducer<decimal?> Min<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, decimal?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the minimum value within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <typeparam name="TResult">The resulting message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum values.</returns>
        public static IProducer<TResult> Min<TSource, TResult>(this IProducer<IEnumerable<TSource>> source, Func<TSource, TResult> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Min(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (int) values.</returns>
        public static IProducer<int> Max(this IProducer<IEnumerable<int>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum nullable int within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable int) values.</returns>
        public static IProducer<int?> Max(this IProducer<IEnumerable<int?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum long within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (long) values.</returns>
        public static IProducer<long> Max(this IProducer<IEnumerable<long>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum nullable long within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable long) values.</returns>
        public static IProducer<long?> Max(this IProducer<IEnumerable<long?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum double within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (double) values.</returns>
        public static IProducer<double> Max(this IProducer<IEnumerable<double>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Max(xs), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum nullable double within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable double) values.</returns>
        public static IProducer<double?> Max(this IProducer<IEnumerable<double?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Max(xs), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum float within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (float) values.</returns>
        public static IProducer<float> Max(this IProducer<IEnumerable<float>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Max(xs), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum nullable float within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable float) values.</returns>
        public static IProducer<float?> Max(this IProducer<IEnumerable<float?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Max(xs), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum decimal within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (decimal) values.</returns>
        public static IProducer<decimal> Max(this IProducer<IEnumerable<decimal>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum nullable decimal within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable decimal) values.</returns>
        public static IProducer<decimal?> Max(this IProducer<IEnumerable<decimal?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the maximum value within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum values.</returns>
        public static IProducer<TSource> Max<TSource>(this IProducer<IEnumerable<TSource>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum int within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (int) values.</returns>
        public static IProducer<int> Max<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, int> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum nullable int within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable int) values.</returns>
        public static IProducer<int?> Max<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, int?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum long within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (long) values.</returns>
        public static IProducer<long> Max<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, long> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum nullable long within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable long) values.</returns>
        public static IProducer<long?> Max<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, long?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum float within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (float) values.</returns>
        public static IProducer<float> Max<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, float> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Max(xs.Select(selector)), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum nullable float within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable float) values.</returns>
        public static IProducer<float?> Max<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, float?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Max(xs.Select(selector)), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum double within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (double) values.</returns>
        public static IProducer<double> Max<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, double> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Max(xs.Select(selector)), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum nullable double within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable double) values.</returns>
        public static IProducer<double?> Max<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, double?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => MathExtensions.Max(xs.Select(selector)), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum decimal within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (decimal) values.</returns>
        public static IProducer<decimal> Max<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, decimal> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum nullable decimal within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable decimal) values.</returns>
        public static IProducer<decimal?> Max<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, decimal?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the maximum value within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <typeparam name="TResult">The resulting message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum values.</returns>
        public static IProducer<TResult> Max<TSource, TResult>(this IProducer<IEnumerable<TSource>> source, Func<TSource, TResult> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Max(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute the average (double) within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<IEnumerable<int>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the average (nullable double) within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average(this IProducer<IEnumerable<int?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the average (double) within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<IEnumerable<long>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the average (nullable double) within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average(this IProducer<IEnumerable<long?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the average (float) within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (float) values.</returns>
        public static IProducer<float> Average(this IProducer<IEnumerable<float>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the average (nullable float) within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable float) values.</returns>
        public static IProducer<float?> Average(this IProducer<IEnumerable<float?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the average (double) within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<IEnumerable<double>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the average (nullable double) within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average(this IProducer<IEnumerable<double?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the average (decimal) within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (decimal) values.</returns>
        public static IProducer<decimal> Average(this IProducer<IEnumerable<decimal>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(), deliveryPolicy);
        }

        /// <summary>
        /// Compute the average (nullable decimal) within each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable decimal) values.</returns>
        public static IProducer<decimal?> Average(this IProducer<IEnumerable<decimal?>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the average (double) within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, int> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the average (nullable double) within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, int?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the average (double) within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, long> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the average (nullable double) within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, long?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the average (float) within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (float) values.</returns>
        public static IProducer<float> Average<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, float> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the average (nullable float) within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable float) values.</returns>
        public static IProducer<float?> Average<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, float?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the average (double) within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, double> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the average (nullable double) within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, double?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the average (decimal) within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (decimal) values.</returns>
        public static IProducer<decimal> Average<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, decimal> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(selector), deliveryPolicy);
        }

        /// <summary>
        /// Invoke a transform function on each element and compute the average (nullable decimal) within each window.
        /// </summary>
        /// <typeparam name="TSource">The source message type.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="selector">Transform function applied to each element.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable decimal) values.</returns>
        public static IProducer<decimal?> Average<TSource>(this IProducer<IEnumerable<TSource>> source, Func<TSource, decimal?> selector, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Average(selector), deliveryPolicy);
        }

        /// <summary>
        /// Compute standard deviation of (float) values.
        /// </summary>
        /// <param name="source">Source stream.</param>
        /// <returns>Stream of standard deviation (float) values.</returns>
        public static float Std(this IEnumerable<float> source)
        {
            var count = source.Count();
            if (count < 1)
            {
                return 0f;
            }

            return (float)Math.Sqrt(source.Sum(d => Math.Pow(d - source.Average(), 2)) / (count - 1));
        }

        /// <summary>
        /// Compute standard deviation of (double) values.
        /// </summary>
        /// <param name="source">Source stream.</param>
        /// <returns>Stream of standard deviation (double) values.</returns>
        public static double Std(this IEnumerable<double> source)
        {
            var count = source.Count();
            if (count < 1)
            {
                return 0;
            }

            return Math.Sqrt(source.Sum(d => Math.Pow(d - source.Average(), 2)) / (count - 1));
        }

        /// <summary>
        /// Compute standard deviation of (decimal) values.
        /// </summary>
        /// <param name="source">Source stream.</param>
        /// <returns>Stream of standard deviation (decimal) values.</returns>
        public static decimal Std(this IEnumerable<decimal> source)
        {
            var count = source.Count();
            if (count < 1)
            {
                return 0;
            }

            Func<decimal, decimal> sq = x => x * x;

            return (decimal)Math.Sqrt((double)source.Sum(d => sq(d - source.Average())) / (count - 1));
        }

        /// <summary>
        /// Compute standard deviation of each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of std values.</returns>
        public static IProducer<float> Std(this IProducer<IEnumerable<float>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Std(), deliveryPolicy);
        }

        /// <summary>
        /// Compute standard deviation of each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of std values.</returns>
        public static IProducer<double> Std(this IProducer<IEnumerable<double>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Std(), deliveryPolicy);
        }

        /// <summary>
        /// Compute standard deviation of each window.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of std values.</returns>
        public static IProducer<decimal> Std(this IProducer<IEnumerable<decimal>> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(xs => xs.Std(), deliveryPolicy);
        }

#endregion LINQ
#region Over History

        /// <summary>
        /// Compute the sum (decimal) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (decimal) values.</returns>
        public static IProducer<decimal> Sum(this IProducer<decimal> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (decimal) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (decimal) values.</returns>
        public static IProducer<decimal> Sum(this IProducer<decimal> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (nullable decimal) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable decimal) values.</returns>
        public static IProducer<decimal?> Sum(this IProducer<decimal?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (nullable decimal) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable decimal) values.</returns>
        public static IProducer<decimal?> Sum(this IProducer<decimal?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (double) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (double) values.</returns>
        public static IProducer<double> Sum(this IProducer<double> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (double) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (double) values.</returns>
        public static IProducer<double> Sum(this IProducer<double> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (nullable double) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable double) values.</returns>
        public static IProducer<double?> Sum(this IProducer<double?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (nullable double) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable double) values.</returns>
        public static IProducer<double?> Sum(this IProducer<double?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (float) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (float) values.</returns>
        public static IProducer<float> Sum(this IProducer<float> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (float) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (float) values.</returns>
        public static IProducer<float> Sum(this IProducer<float> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (nullable float) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable float) values.</returns>
        public static IProducer<float?> Sum(this IProducer<float?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (nullable float) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable float) values.</returns>
        public static IProducer<float?> Sum(this IProducer<float?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (int) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (int) values.</returns>
        public static IProducer<int> Sum(this IProducer<int> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (int) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (int) values.</returns>
        public static IProducer<int> Sum(this IProducer<int> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (nullable int) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable int) values.</returns>
        public static IProducer<int?> Sum(this IProducer<int?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (nullable int) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable int) values.</returns>
        public static IProducer<int?> Sum(this IProducer<int?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (long) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (long) values.</returns>
        public static IProducer<long> Sum(this IProducer<long> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (long) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (long) values.</returns>
        public static IProducer<long> Sum(this IProducer<long> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (nullable long) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable long) values.</returns>
        public static IProducer<long?> Sum(this IProducer<long?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the sum (nullable long) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of sum (nullable long) values.</returns>
        public static IProducer<long?> Sum(this IProducer<long?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Sum(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (decimal) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (decimal) values.</returns>
        public static IProducer<decimal> Min(this IProducer<decimal> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (decimal) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (decimal) values.</returns>
        public static IProducer<decimal> Min(this IProducer<decimal> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (nullable decimal) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable decimal) values.</returns>
        public static IProducer<decimal?> Min(this IProducer<decimal?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (nullable decimal) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable decimal) values.</returns>
        public static IProducer<decimal?> Min(this IProducer<decimal?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (double) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (double) values.</returns>
        public static IProducer<double> Min(this IProducer<double> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (double) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (double) values.</returns>
        public static IProducer<double> Min(this IProducer<double> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (nullable double) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable double) values.</returns>
        public static IProducer<double?> Min(this IProducer<double?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (nullable double) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable double) values.</returns>
        public static IProducer<double?> Min(this IProducer<double?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (float) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (float) values.</returns>
        public static IProducer<float> Min(this IProducer<float> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (float) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (float) values.</returns>
        public static IProducer<float> Min(this IProducer<float> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (nullable float) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable float) values.</returns>
        public static IProducer<float?> Min(this IProducer<float?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (nullable float) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable float) values.</returns>
        public static IProducer<float?> Min(this IProducer<float?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (int) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<int> Min(this IProducer<int> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (int) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (int) values.</returns>
        public static IProducer<int> Min(this IProducer<int> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (nullable int) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable int) values.</returns>
        public static IProducer<int?> Min(this IProducer<int?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (nullable long) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable long) values.</returns>
        public static IProducer<int?> Min(this IProducer<int?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (long) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (long) values.</returns>
        public static IProducer<long> Min(this IProducer<long> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (long) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (long) values.</returns>
        public static IProducer<long> Min(this IProducer<long> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (nullable long) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable long) values.</returns>
        public static IProducer<long?> Min(this IProducer<long?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (nullable long) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (nullable long) values.</returns>
        public static IProducer<long?> Min(this IProducer<long?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Min(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the minimum (decimal) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of minimum (decimal) values.</returns>
        public static IProducer<decimal> Max(this IProducer<decimal> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (decimal) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (decimal) values.</returns>
        public static IProducer<decimal> Max(this IProducer<decimal> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (nullable decimal) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable decimal) values.</returns>
        public static IProducer<decimal?> Max(this IProducer<decimal?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (nullable decimal) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable decimal) values.</returns>
        public static IProducer<decimal?> Max(this IProducer<decimal?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (double) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (double) values.</returns>
        public static IProducer<double> Max(this IProducer<double> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (double) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (double) values.</returns>
        public static IProducer<double> Max(this IProducer<double> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (nullable double) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable double) values.</returns>
        public static IProducer<double?> Max(this IProducer<double?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (nullable double) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable double) values.</returns>
        public static IProducer<double?> Max(this IProducer<double?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (float) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (float) values.</returns>
        public static IProducer<float> Max(this IProducer<float> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (float) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (float) values.</returns>
        public static IProducer<float> Max(this IProducer<float> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (nullable float) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable float) values.</returns>
        public static IProducer<float?> Max(this IProducer<float?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (nullable float) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable float) values.</returns>
        public static IProducer<float?> Max(this IProducer<float?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (int) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (int) values.</returns>
        public static IProducer<int> Max(this IProducer<int> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (int) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (int) values.</returns>
        public static IProducer<int> Max(this IProducer<int> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (nullable int) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable int) values.</returns>
        public static IProducer<int?> Max(this IProducer<int?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (nullable int) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable int) values.</returns>
        public static IProducer<int?> Max(this IProducer<int?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (long) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (long) values.</returns>
        public static IProducer<long> Max(this IProducer<long> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (long) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (long) values.</returns>
        public static IProducer<long> Max(this IProducer<long> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (nullable long) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable long) values.</returns>
        public static IProducer<long?> Max(this IProducer<long?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the maximum (nullable long) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of maximum (nullable long) values.</returns>
        public static IProducer<long?> Max(this IProducer<long?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Max(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (decimal) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (decimal) values.</returns>
        public static IProducer<decimal> Average(this IProducer<decimal> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (decimal) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (decimal) values.</returns>
        public static IProducer<decimal> Average(this IProducer<decimal> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (nullable decimal) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable decimal) values.</returns>
        public static IProducer<decimal?> Average(this IProducer<decimal?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (nullable decimal) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable decimal) values.</returns>
        public static IProducer<decimal?> Average(this IProducer<decimal?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (double) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<double> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (double) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<double> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (nullable double) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average(this IProducer<double?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (nullable double) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average(this IProducer<double?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (float) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (float) values.</returns>
        public static IProducer<float> Average(this IProducer<float> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (float) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (float) values.</returns>
        public static IProducer<float> Average(this IProducer<float> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (nullable float) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable float) values.</returns>
        public static IProducer<float?> Average(this IProducer<float?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (nullable float) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable float) values.</returns>
        public static IProducer<float?> Average(this IProducer<float?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (int) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<int> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (int) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<int> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (nullable int) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average(this IProducer<int?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (nullable int) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average(this IProducer<int?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (long) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<long> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (long) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (double) values.</returns>
        public static IProducer<double> Average(this IProducer<long> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (nullable long) within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average(this IProducer<long?> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the average (nullable long) within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of average (nullable double) values.</returns>
        public static IProducer<double?> Average(this IProducer<long?> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Average(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the standard deviation within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of standard deviation values.</returns>
        public static IProducer<float> Std(this IProducer<float> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Std(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the standard deviation within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of standard deviation values.</returns>
        public static IProducer<float> Std(this IProducer<float> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Std(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the standard deviation within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of standard deviation values.</returns>
        public static IProducer<double> Std(this IProducer<double> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Std(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the standard deviation within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of standard deviation values.</returns>
        public static IProducer<double> Std(this IProducer<double> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Std(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the standard deviation within each window by time span.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="timeSpan">Window time span.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of standard deviation values.</returns>
        public static IProducer<decimal> Std(this IProducer<decimal> source, TimeSpan timeSpan, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, RelativeTimeInterval.Past(timeSpan), deliveryPolicy).Std(DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Compute the standard deviation within each window by size.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="size">Window size.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of standard deviation values.</returns>
        public static IProducer<decimal> Std(this IProducer<decimal> source, int size, DeliveryPolicy deliveryPolicy = null)
        {
            return Window(source, -(size - 1), 0, deliveryPolicy).Std(DeliveryPolicy.Unlimited);
        }

#endregion Over History
#region Miscellaneous

        /// <summary>
        /// Map (double) values to absolute value.
        /// </summary>
        /// <param name="source">Source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of absolute (double) values.</returns>
        public static IProducer<double> Abs(this IProducer<double> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(d => Math.Abs(d), deliveryPolicy);
        }

        /// <summary>
        /// Compute delta (double) value between successive stream values.
        /// </summary>
        /// <param name="source">Source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of delta (double) values.</returns>
        public static IProducer<double> Delta(this IProducer<double> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Window(0, 1, deliveryPolicy).Select(b => b.ToArray(), DeliveryPolicy.Unlimited).Select(p => (p.Length > 1 ? p[1] : 0) - (p.Length > 0 ? p[0] : 0), DeliveryPolicy.Unlimited);
        }

        /// <summary>
        /// Map stream (double) values to natural (base e) logarithm.
        /// </summary>
        /// <param name="source">Source stream.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of natural (base e) logarithms.</returns>
        public static IProducer<double> Log(this IProducer<double> source, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(Math.Log, deliveryPolicy);
        }

        /// <summary>
        /// Map stream (double) values to logarithm in given base.
        /// </summary>
        /// <param name="source">Source stream.</param>
        /// <param name="newBase">The base of the logarithm.</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Stream of logarithms in given base.</returns>
        public static IProducer<double> Log(this IProducer<double> source, double newBase, DeliveryPolicy deliveryPolicy = null)
        {
            return source.Select(x => Math.Log(x, newBase), deliveryPolicy);
        }

#endregion Miscellaneous
    }
}
