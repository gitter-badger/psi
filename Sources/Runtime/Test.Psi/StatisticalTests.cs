﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Test.Psi
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Psi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StatisticalTests
    {
        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMinDouble()
        {
            this.RunTest(Operators.Min,
                (
                    new double[] { }, // empty sequence
                    new double[] { } // expected output
                ),
                (
                    new[] { 0.0, 1.0, 2.0, -1.0, -2.0, 3.0 }, // real numbers only
                    new[] { 0.0, 0.0, 0.0, -1.0, -2.0, -2.0 } // expected output
                ),
                (
                    new[] { 1.0, double.NaN, 0.0, 1.0 }, // sequence with NaN
                    new[] { 1.0, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { double.NaN, 1.0, 0.0, 1.0 }, // first element is NaN
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { 1.0, 0.0, 1.0, double.NaN }, // last element is NaN
                    new[] { 1.0, 0.0, 0.0, double.NaN } // expected output
                ),
                (
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN }, // sequence contains only NaNs
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { double.PositiveInfinity, 1.0, double.NegativeInfinity, -1.0, double.NaN }, // sequence with +/- infinity
                    new[] { double.PositiveInfinity, 1.0, double.NegativeInfinity, double.NegativeInfinity, double.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMinIEnumerableDouble()
        {
            this.RunTest(Operators.Min,
                (
                    new IEnumerable<double>[] // sequence of enumerations
                    {
                        new[] { 1.0 },
                        new[] { 1.0, 2.0 },
                        new[] { 1.0, double.NaN, -1.0 },
                        new[] { double.NaN, 2.0, -1.0 },
                        new[] { double.NegativeInfinity, 2.0, -1.0 },
                        new[] { double.PositiveInfinity, 2.0, -1.0 }
                    },
                    new[] { 1.0, 1.0, double.NaN, double.NaN, double.NegativeInfinity, -1.0 } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMinEmptyIEnumerableDouble()
        {
            Exception error = null;

            try
            {
                this.RunTest(Operators.Min,
                    (
                        new IEnumerable<double>[] { new double[] { } }, // sequence containing an empty enumeration
                        new double[] { } // no expected output - should throw
                    )
                );
            }
            catch (Exception e)
            {
                error = e;
            }

            Assert.IsNotNull(error);
            Assert.IsInstanceOfType(error, typeof(AggregateException));
            Assert.IsInstanceOfType(error.InnerException, typeof(InvalidOperationException));
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMinIEnumerableNullableDouble()
        {
            this.RunTest(Operators.Min,
                (
                    new IEnumerable<double?>[] // sequence of enumerations
                    {
                        new double?[] { },
                        new double?[] { null },
                        new double?[] { null, 1.0, null },
                        new double?[] { null, 1.0, null, 2.0, null },
                        new double?[] { null, 1.0, null, double.NaN, null, -1.0, null },
                        new double?[] { null, double.NaN, null, 2.0, null, -1.0, null },
                        new double?[] { null, double.NegativeInfinity, null, 2.0, null, -1.0, null },
                        new double?[] { null, double.PositiveInfinity, null, 2.0, null, -1.0, null }
                    },
                    new double?[] { null, null, 1.0, 1.0, double.NaN, double.NaN, double.NegativeInfinity, -1.0 } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMinFloat()
        {
            this.RunTest(Operators.Min,
                (
                    new float[] { }, // empty sequence
                    new float[] { } // expected output
                ),
                (
                    new[] { 0.0f, 1.0f, 2.0f, -1.0f, -2.0f, 3.0f }, // real numbers only
                    new[] { 0.0f, 0.0f, 0.0f, -1.0f, -2.0f, -2.0f } // expected output
                ),
                (
                    new[] { 1.0f, float.NaN, 0.0f, 1.0f }, // sequence with NaN
                    new[] { 1.0f, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { float.NaN, 1.0f, 0.0f, 1.0f }, // first element is NaN
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { 1.0f, 0.0f, 1.0f, float.NaN }, // last element is NaN
                    new[] { 1.0f, 0.0f, 0.0f, float.NaN } // expected output
                ),
                (
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN }, // sequence contains only NaNs
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { float.PositiveInfinity, 1.0f, float.NegativeInfinity, -1.0f, float.NaN }, // sequence with +/- infinity
                    new[] { float.PositiveInfinity, 1.0f, float.NegativeInfinity, float.NegativeInfinity, float.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMinIEnumerableFloat()
        {
            this.RunTest(Operators.Min,
                (
                    new IEnumerable<float>[] // sequence of enumerations
                    {
                        new[] { 1.0f },
                        new[] { 1.0f, 2.0f },
                        new[] { 1.0f, float.NaN, -1.0f },
                        new[] { float.NaN, 2.0f, -1.0f },
                        new[] { float.NegativeInfinity, 2.0f, -1.0f },
                        new[] { float.PositiveInfinity, 2.0f, -1.0f }
                    },
                    new[] { 1.0f, 1.0f, float.NaN, float.NaN, float.NegativeInfinity, -1.0f } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMinEmptyIEnumerableFloat()
        {
            Exception error = null;

            try
            {
                this.RunTest(Operators.Min,
                    (
                        new IEnumerable<float>[] { new float[] { } }, // sequence containing an empty enumeration
                        new float[] { } // no expected output - should throw
                    )
                );
            }
            catch (Exception e)
            {
                error = e;
            }

            Assert.IsNotNull(error);
            Assert.IsInstanceOfType(error, typeof(AggregateException));
            Assert.IsInstanceOfType(error.InnerException, typeof(InvalidOperationException));
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMinIEnumerableNullableFloat()
        {
            this.RunTest(Operators.Min,
                (
                    new IEnumerable<float?>[] // sequence of enumerations
                    {
                        new float?[] { },
                        new float?[] { null },
                        new float?[] { null, 1.0f, null },
                        new float?[] { null, 1.0f, null, 2.0f, null },
                        new float?[] { null, 1.0f, null, float.NaN, null, -1.0f, null },
                        new float?[] { null, float.NaN, null, 2.0f, null, -1.0f, null },
                        new float?[] { null, float.NegativeInfinity, null, 2.0f, null, -1.0f, null },
                        new float?[] { null, float.PositiveInfinity, null, 2.0f, null, -1.0f, null }
                    },
                    new float?[] { null, null, 1.0f, 1.0f, float.NaN, float.NaN, float.NegativeInfinity, -1.0f } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMaxDouble()
        {
            this.RunTest(Operators.Max,
                (
                    new double[] { }, // empty sequence
                    new double[] { } // expected output
                ),
                (
                    new[] { -1.0, -2.0, -3.0, 0.0, 1.0, 2.0 }, // real numbers only
                    new[] { -1.0, -1.0, -1.0, 0.0, 1.0, 2.0 } // expected output
                ),
                (
                    new[] { 0.0, double.NaN, 1.0, 0.0 }, // sequence with NaN
                    new[] { 0.0, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { double.NaN, 0.0, 1.0, 0.0 }, // first element is NaN
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { 0.0, 1.0, 0.0, double.NaN }, // last element is NaN
                    new[] { 0.0, 1.0, 1.0, double.NaN } // expected output
                ),
                (
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN }, // sequence contains only NaNs
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { double.NegativeInfinity, -1.0, double.PositiveInfinity, 1.0, double.NaN }, // sequence with +/- infinity
                    new[] { double.NegativeInfinity, -1.0, double.PositiveInfinity, double.PositiveInfinity, double.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMaxIEnumerableDouble()
        {
            this.RunTest(Operators.Max,
                (
                    new IEnumerable<double>[] // sequence of enumerations
                    {
                        new[] { 1.0 },
                        new[] { 1.0, 2.0 },
                        new[] { 1.0, double.NaN, -1.0 },
                        new[] { double.NaN, 2.0, -1.0 },
                        new[] { double.NegativeInfinity, 2.0, -1.0 },
                        new[] { double.PositiveInfinity, 2.0, -1.0 }
                    },
                    new[] { 1.0, 2.0, double.NaN, double.NaN, 2.0, double.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMaxEmptyIEnumerableDouble()
        {
            Exception error = null;

            try
            {
                this.RunTest(Operators.Max,
                    (
                        new IEnumerable<double>[] { new double[] { } }, // sequence containing an empty enumeration
                        new double[] { } // no expected output - should throw
                    )
                );
            }
            catch (Exception e)
            {
                error = e;
            }

            Assert.IsNotNull(error);
            Assert.IsInstanceOfType(error, typeof(AggregateException));
            Assert.IsInstanceOfType(error.InnerException, typeof(InvalidOperationException));
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMaxIEnumerableNullableDouble()
        {
            this.RunTest(Operators.Max,
                (
                    new IEnumerable<double?>[] // sequence of enumerations
                    {
                        new double?[] { },
                        new double?[] { null },
                        new double?[] { null, 1.0, null },
                        new double?[] { null, 1.0, null, 2.0, null },
                        new double?[] { null, 1.0, null, double.NaN, null, -1.0, null },
                        new double?[] { null, double.NaN, null, 2.0, null, -1.0, null },
                        new double?[] { null, double.NegativeInfinity, null, 2.0, null, -1.0, null },
                        new double?[] { null, double.PositiveInfinity, null, 2.0, null, -1.0, null }
                    },
                    new double?[] { null, null, 1.0, 2.0, double.NaN, double.NaN, 2.0, double.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMaxFloat()
        {
            this.RunTest(Operators.Max,
                (
                    new float[] { }, // empty sequence
                    new float[] { } // expected output
                ),
                (
                    new[] { -1.0f, -2.0f, -3.0f, 0.0f, 1.0f, 2.0f }, // real numbers only
                    new[] { -1.0f, -1.0f, -1.0f, 0.0f, 1.0f, 2.0f } // expected output
                ),
                (
                    new[] { 0.0f, float.NaN, 1.0f, 0.0f }, // sequence with NaN
                    new[] { 0.0f, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { float.NaN, 0.0f, 1.0f, 0.0f }, // first element is NaN
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { 0.0f, 1.0f, 0.0f, float.NaN }, // last element is NaN
                    new[] { 0.0f, 1.0f, 1.0f, float.NaN } // expected output
                ),
                (
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN }, // sequence contains only NaNs
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { float.NegativeInfinity, -1.0f, float.PositiveInfinity, 1.0f, float.NaN }, // sequence with +/- infinity
                    new[] { float.NegativeInfinity, -1.0f, float.PositiveInfinity, float.PositiveInfinity, float.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMaxIEnumerableFloat()
        {
            this.RunTest(Operators.Max,
                (
                    new IEnumerable<float>[] // sequence of enumerations
                    {
                        new[] { 1.0f },
                        new[] { 1.0f, 2.0f },
                        new[] { 1.0f, float.NaN, -1.0f },
                        new[] { float.NaN, 2.0f, -1.0f },
                        new[] { float.NegativeInfinity, 2.0f, -1.0f },
                        new[] { float.PositiveInfinity, 2.0f, -1.0f }
                    },
                    new[] { 1.0f, 2.0f, float.NaN, float.NaN, 2.0f, float.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMaxEmptyIEnumerableFloat()
        {
            Exception error = null;

            try
            {
                this.RunTest(Operators.Max,
                    (
                        new IEnumerable<float>[] { new float[] { } }, // sequence containing an empty enumeration
                        new float[] { } // no expected output - should throw
                    )
                );
            }
            catch (Exception e)
            {
                error = e;
            }

            Assert.IsNotNull(error);
            Assert.IsInstanceOfType(error, typeof(AggregateException));
            Assert.IsInstanceOfType(error.InnerException, typeof(InvalidOperationException));
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsMaxIEnumerableNullableFloat()
        {
            this.RunTest(Operators.Max,
                (
                    new IEnumerable<float?>[] // sequence of enumerations
                    {
                        new float?[] { },
                        new float?[] { null },
                        new float?[] { null, 1.0f, null },
                        new float?[] { null, 1.0f, null, 2.0f, null },
                        new float?[] { null, 1.0f, null, float.NaN, null, -1.0f, null },
                        new float?[] { null, float.NaN, null, 2.0f, null, -1.0f, null },
                        new float?[] { null, float.NegativeInfinity, null, 2.0f, null, -1.0f, null },
                        new float?[] { null, float.PositiveInfinity, null, 2.0f, null, -1.0f, null }
                    },
                    new float?[] { null, null, 1.0f, 2.0f, float.NaN, float.NaN, 2.0f, float.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsSumDouble()
        {
            this.RunTest(Operators.Sum,
                (
                    new double[] { }, // empty sequence
                    new double[] { } // expected output
                ),
                (
                    new[] { -1.0, -2.0, -3.0, 0.0, 1.0, 2.0 }, // real numbers only
                    new[] { -1.0, -3.0, -6.0, -6.0, -5.0, -3.0 } // expected output
                ),
                (
                    new[] { 0.0, double.NaN, 1.0, 0.0 }, // sequence with NaN
                    new[] { 0.0, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { double.NaN, 0.0, 1.0, 0.0 }, // first element is NaN
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { 0.0, 1.0, 0.0, double.NaN }, // last element is NaN
                    new[] { 0.0, 1.0, 1.0, double.NaN } // expected output
                ),
                (
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN }, // sequence contains only NaNs
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { double.NegativeInfinity, -1.0, double.PositiveInfinity, 1.0, double.NaN }, // sequence with +/- infinity
                    new[] { double.NegativeInfinity, double.NegativeInfinity, double.NaN, double.NaN, double.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsSumNullableDouble()
        {
            this.RunTest(Operators.Sum,
                (
                    new double?[] { }, // empty sequence
                    new double?[] { } // expected output
                ),
                (
                    new double?[] { null, null, null }, // null sequence
                    new double?[] { 0, 0, 0 } // expected output
                ),
                (
                    new double?[] { -1.0, -2.0, -3.0, 0.0, 1.0, 2.0 }, // real numbers only
                    new double?[] { -1.0, -3.0, -6.0, -6.0, -5.0, -3.0 } // expected output
                ),
                (
                    new double?[] { -1.0, -2.0, null, -3.0, 0.0, 1.0, null, 2.0 }, // sequence with null values
                    new double?[] { -1.0, -3.0, -3.0, -6.0, -6.0, -5.0, -5.0, -3.0 } // expected output
                ),
                (
                    new double?[] { 0.0, double.NaN, 1.0, 0.0 }, // sequence with NaN
                    new double?[] { 0.0, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new double?[] { double.NaN, 0.0, 1.0, 0.0 }, // first element is NaN
                    new double?[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new double?[] { 0.0, 1.0, 0.0, double.NaN }, // last element is NaN
                    new double?[] { 0.0, 1.0, 1.0, double.NaN } // expected output
                ),
                (
                    new double?[] { double.NaN, double.NaN, double.NaN, double.NaN }, // sequence contains only NaNs
                    new double?[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new double?[] { double.NegativeInfinity, -1.0, double.PositiveInfinity, 1.0, double.NaN }, // sequence with +/- infinity
                    new double?[] { double.NegativeInfinity, double.NegativeInfinity, double.NaN, double.NaN, double.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsSumIEnumerableDouble()
        {
            this.RunTest(Operators.Sum,
                (
                    new IEnumerable<double>[] // sequence of enumerations
                    {
                        new double[] { },
                        new[] { 1.0, 2.0, -1.5, 3.0 },
                        new[] { 1.0, double.NaN, -1.5, 3.0 },
                        new[] { double.NaN, 2.0, -1.5, 3.0 },
                        new[] { double.NegativeInfinity, 2.0, -1.5, 3.0 },
                        new[] { double.PositiveInfinity, 2.0, -1.5, 3.0 }
                    },
                    new[] { 0, 4.5, double.NaN, double.NaN, double.NegativeInfinity, double.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsSumIEnumerableNullableDouble()
        {
            this.RunTest(Operators.Sum,
                (
                    new IEnumerable<double?>[] // sequence of enumerations
                    {
                        new double?[] { },
                        new double?[] { null },
                        new double?[] { null, 1.0, null, 2.0, null, -1.5, null, 3.0, null },
                        new double?[] { null, 1.0, null, double.NaN, null, -1.5, null, 3.0, null },
                        new double?[] { null, double.NaN, null, 2.0, null, -1.5, null, 3.0, null },
                        new double?[] { null, double.NegativeInfinity, null, 2.0, null, -1.5, null, 3.0, null },
                        new double?[] { null, double.PositiveInfinity, null, 2.0, null, -1.5, null, 3.0, null }
                    },
                    new double?[] { 0, 0, 4.5, double.NaN, double.NaN, double.NegativeInfinity, double.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsSumFloat()
        {
            this.RunTest(Operators.Sum,
                (
                    new float[] { }, // empty sequence
                    new float[] { } // expected output
                ),
                (
                    new[] { -1.0f, -2.0f, -3.0f, 0.0f, 1.0f, 2.0f }, // real numbers only
                    new[] { -1.0f, -3.0f, -6.0f, -6.0f, -5.0f, -3.0f } // expected output
                ),
                (
                    new[] { 0.0f, float.NaN, 1.0f, 0.0f }, // sequence with NaN
                    new[] { 0.0f, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { float.NaN, 0.0f, 1.0f, 0.0f }, // first element is NaN
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { 0.0f, 1.0f, 0.0f, float.NaN }, // last element is NaN
                    new[] { 0.0f, 1.0f, 1.0f, float.NaN } // expected output
                ),
                (
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN }, // sequence contains only NaNs
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { float.NegativeInfinity, -1.0f, float.PositiveInfinity, 1.0f, float.NaN }, // sequence with +/- infinity
                    new[] { float.NegativeInfinity, float.NegativeInfinity, float.NaN, float.NaN, float.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsSumNullableFloat()
        {
            this.RunTest(Operators.Sum,
                (
                    new float?[] { }, // empty sequence
                    new float?[] { } // expected output
                ),
                (
                    new float?[] { null, null, null }, // null sequence
                    new float?[] { 0, 0, 0 } // expected output
                ),
                (
                    new float?[] { -1.0f, -2.0f, -3.0f, 0.0f, 1.0f, 2.0f }, // real numbers only
                    new float?[] { -1.0f, -3.0f, -6.0f, -6.0f, -5.0f, -3.0f } // expected output
                ),
                (
                    new float?[] { -1.0f, -2.0f, null, -3.0f, 0.0f, 1.0f, null, 2.0f }, // sequence with null values
                    new float?[] { -1.0f, -3.0f, -3.0f, -6.0f, -6.0f, -5.0f, -5.0f, -3.0f } // expected output
                ),
                (
                    new float?[] { 0.0f, float.NaN, 1.0f, 0.0f }, // sequence with NaN
                    new float?[] { 0.0f, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new float?[] { float.NaN, 0.0f, 1.0f, 0.0f }, // first element is NaN
                    new float?[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new float?[] { 0.0f, 1.0f, 0.0f, float.NaN }, // last element is NaN
                    new float?[] { 0.0f, 1.0f, 1.0f, float.NaN } // expected output
                ),
                (
                    new float?[] { float.NaN, float.NaN, float.NaN, float.NaN }, // sequence contains only NaNs
                    new float?[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new float?[] { float.NegativeInfinity, -1.0f, float.PositiveInfinity, 1.0f, float.NaN }, // sequence with +/- infinity
                    new float?[] { float.NegativeInfinity, float.NegativeInfinity, float.NaN, float.NaN, float.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsSumIEnumerableFloat()
        {
            this.RunTest(Operators.Sum,
                (
                    new IEnumerable<float>[] // sequence of enumerations
                    {
                        new float[] { },
                        new[] { 1.0f, 2.0f, -1.5f, 3.0f },
                        new[] { 1.0f, float.NaN, -1.5f, 3.0f },
                        new[] { float.NaN, 2.0f, -1.5f, 3.0f },
                        new[] { float.NegativeInfinity, 2.0f, -1.5f, 3.0f },
                        new[] { float.PositiveInfinity, 2.0f, -1.5f, 3.0f }
                    },
                    new[] { 0f, 4.5f, float.NaN, float.NaN, float.NegativeInfinity, float.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsSumIEnumerableNullableFloat()
        {
            this.RunTest(Operators.Sum,
                (
                    new IEnumerable<float?>[] // sequence of enumerations
                    {
                        new float?[] { },
                        new float?[] { null },
                        new float?[] { null, 1.0f, null, 2.0f, null, -1.5f, null, 3.0f, null },
                        new float?[] { null, 1.0f, null, float.NaN, null, -1.5f, null, 3.0f, null },
                        new float?[] { null, float.NaN, null, 2.0f, null, -1.5f, null, 3.0f, null },
                        new float?[] { null, float.NegativeInfinity, null, 2.0f, null, -1.5f, null, 3.0f, null },
                        new float?[] { null, float.PositiveInfinity, null, 2.0f, null, -1.5f, null, 3.0f, null }
                    },
                    new float?[] { 0f, 0f, 4.5f, float.NaN, float.NaN, float.NegativeInfinity, float.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsAverageDouble()
        {
            this.RunTest(Operators.Average,
                (
                    new double[] { }, // empty sequence
                    new double[] { } // expected output
                ),
                (
                    new[] { -1.0, -2.0, -3.0, 0.0, 1.0, 2.0 }, // real numbers only
                    new[] { -1.0, -1.5, -2.0, -1.5, -1.0, -0.5 } // expected output
                ),
                (
                    new[] { 0.0, double.NaN, 1.0, 0.0 }, // sequence with NaN
                    new[] { 0.0, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { double.NaN, 0.0, 1.0, 0.0 }, // first element is NaN
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { 0.0, 1.0, 0.0, double.NaN }, // last element is NaN
                    new[] { 0.0, 0.5, 1.0 / 3.0, double.NaN } // expected output
                ),
                (
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN }, // sequence contains only NaNs
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { double.NegativeInfinity, -1.0, double.PositiveInfinity, 1.0, double.NaN }, // sequence with +/- infinity
                    new[] { double.NegativeInfinity, double.NegativeInfinity, double.NaN, double.NaN, double.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsAverageIEnumerableDouble()
        {
            this.RunTest(Operators.Average,
                (
                    new IEnumerable<double>[] // sequence of enumerations
                    {
                        new[] { 1.0 },
                        new[] { 1.0, 2.0, -1.5, 3.0 },
                        new[] { 1.0, double.NaN, -1.5, 3.0 },
                        new[] { double.NaN, 2.0, -1.5, 3.0 },
                        new[] { double.NegativeInfinity, 2.0, -1.5, 3.0 },
                        new[] { double.PositiveInfinity, 2.0, -1.5, 3.0 }
                    },
                    new[] { 1.0, 1.125, double.NaN, double.NaN, double.NegativeInfinity, double.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsAverageIEnumerableNullableDouble()
        {
            this.RunTest(Operators.Average,
                (
                    new IEnumerable<double?>[] // sequence of enumerations
                    {
                        new double?[] { },
                        new double?[] { null },
                        new double?[] { null, 1.0, null },
                        new double?[] { null, 1.0, null, 2.0, null, -1.5, null, 3.0, null },
                        new double?[] { null, 1.0, null, double.NaN, null, -1.5, null, 3.0, null },
                        new double?[] { null, double.NaN, null, 2.0, null, -1.5, null, 3.0, null },
                        new double?[] { null, double.NegativeInfinity, null, 2.0, null, -1.5, null, 3.0, null },
                        new double?[] { null, double.PositiveInfinity, null, 2.0, null, -1.5, null, 3.0, null }
                    },
                    new double?[] { null, null, 1.0, 1.125, double.NaN, double.NaN, double.NegativeInfinity, double.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsAverageFloat()
        {
            this.RunTest(Operators.Average,
                (
                    new float[] { }, // empty sequence
                    new float[] { } // expected output
                ),
                (
                    new[] { -1.0f, -2.0f, -3.0f, 0.0f, 1.0f, 2.0f }, // real numbers only
                    new[] { -1.0f, -1.5f, -2.0f, -1.5f, -1.0f, -0.5f } // expected output
                ),
                (
                    new[] { 0.0f, float.NaN, 1.0f, 0.0f }, // sequence with NaN
                    new[] { 0.0f, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { float.NaN, 0.0f, 1.0f, 0.0f }, // first element is NaN
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { 0.0f, 1.0f, 0.0f, float.NaN }, // last element is NaN
                    new[] { 0.0f, 0.5f, 1.0f / 3.0f, float.NaN } // expected output
                ),
                (
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN }, // sequence contains only NaNs
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { float.NegativeInfinity, -1.0f, float.PositiveInfinity, 1.0f, float.NaN }, // sequence with +/- infinity
                    new[] { float.NegativeInfinity, float.NegativeInfinity, float.NaN, float.NaN, float.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsAverageIEnumerableFloat()
        {
            this.RunTest(Operators.Average,
                (
                    new IEnumerable<float>[] // sequence of enumerations
                    {
                        new[] { 1.0f },
                        new[] { 1.0f, 2.0f, -1.5f, 3.0f },
                        new[] { 1.0f, float.NaN, -1.5f, 3.0f },
                        new[] { float.NaN, 2.0f, -1.5f, 3.0f },
                        new[] { float.NegativeInfinity, 2.0f, -1.5f, 3.0f },
                        new[] { float.PositiveInfinity, 2.0f, -1.5f, 3.0f }
                    },
                    new[] { 1.0f, 1.125f, float.NaN, float.NaN, float.NegativeInfinity, float.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsAverageIEnumerableNullableFloat()
        {
            this.RunTest(Operators.Average,
                (
                    new IEnumerable<float?>[] // sequence of enumerations
                    {
                        new float?[] { },
                        new float?[] { null },
                        new float?[] { null, 1.0f, null },
                        new float?[] { null, 1.0f, null, 2.0f, null, -1.5f, null, 3.0f, null },
                        new float?[] { null, 1.0f, null, float.NaN, null, -1.5f, null, 3.0f, null },
                        new float?[] { null, float.NaN, null, 2.0f, null, -1.5f, null, 3.0f, null },
                        new float?[] { null, float.NegativeInfinity, null, 2.0f, null, -1.5f, null, 3.0f, null },
                        new float?[] { null, float.PositiveInfinity, null, 2.0f, null, -1.5f, null, 3.0f, null }
                    },
                    new float?[] { null, null, 1.0f, 1.125f, float.NaN, float.NaN, float.NegativeInfinity, float.PositiveInfinity } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsStdDouble()
        {
            this.RunTest(Operators.Std,
                (
                    new double[] { }, // empty sequence
                    new double[] { }  // expected output
                ),
                (
                    new[] { -1.0, -2.0, -3.0, 0.0, 1.0, 2.0 }, // real numbers only
                    new[] { double.NaN, 0.70710678118654757, 1, 1.2909944487358056, 1.5811388300841898, 1.8708286933869707 } // expected output
                ),
                (
                    new[] { 0.0, double.NaN, 1.0, 0.0 }, // sequence with NaN
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { double.NaN, 0.0, 1.0, 0.0 }, // first element is NaN
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                ),
                (
                    new[] { 0.0, 1.0, 0.0, double.NaN }, // last element is NaN
                    new[] { double.NaN, 0.70710678118654757, 0.57735026918962584, double.NaN } // expected output
                ),
                (
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN }, // sequence contains only NaNs
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN }  // expected output
                ),
                (
                    new[] { double.NegativeInfinity, -1.0, double.PositiveInfinity, 1.0, double.NaN }, // sequence with +/- infinity
                    new[] { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsStdIEnumerableDouble()
        {
            this.RunTest(Operators.Std,
                (
                    new IEnumerable<double>[] // sequence of enumerations
                    {
                        new double[] { },
                        new[] { 1.0 },
                        new[] { 1.0, 2.0 },
                        new[] { 1.0, 2.0, -1.5, 3.0 },
                        new[] { 1.0, double.NaN, -1.5, 3.0 },
                        new[] { double.NaN, 2.0, -1.5, 3.0 },
                        new[] { double.NegativeInfinity, 2.0, -1.5, 3.0 },
                        new[] { double.PositiveInfinity, 2.0, -1.5, 3.0 }
                    },
                    new[] { 0, double.NaN, 0.70710678118654757, 1.9311050377094112, double.NaN, double.NaN, double.NaN, double.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsStdFloat()
        {
            this.RunTest(Operators.Std,
                (
                    new float[] { }, // empty sequence
                    new float[] { } // expected output
                ),
                (
                    new[] { -1.0f, -2.0f, -3.0f, 0.0f, 1.0f, 2.0f }, // real numbers only
                    new[] { float.NaN, 0.707106769f, 1f, 1.29099441f, 1.58113885f, 1.87082875f } // expected output
                ),
                (
                    new[] { 0.0f, float.NaN, 1.0f, 0.0f }, // sequence with NaN
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { float.NaN, 0.0f, 1.0f, 0.0f }, // first element is NaN
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { 0.0f, 1.0f, 0.0f, float.NaN }, // last element is NaN
                    new[] { float.NaN, 0.707106769f, 0.577350259f, float.NaN } // expected output
                ),
                (
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN }, // sequence contains only NaNs
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                ),
                (
                    new[] { float.NegativeInfinity, -1.0f, float.PositiveInfinity, 1.0f, float.NaN }, // sequence with +/- infinity
                    new[] { float.NaN, float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                )
            );
        }

        [TestMethod]
        [Timeout(60000)]
        public void StatisticsStdIEnumerableFloat()
        {
            this.RunTest(Operators.Std,
                (
                    new IEnumerable<float>[] // sequence of enumerations
                    {
                        new float[] { },
                        new[] { 1.0f },
                        new[] { 1.0f, 2.0f },
                        new[] { 1.0f, 2.0f, -1.5f, 3.0f },
                        new[] { 1.0f, float.NaN, -1.5f, 3.0f },
                        new[] { float.NaN, 2.0f, -1.5f, 3.0f },
                        new[] { float.NegativeInfinity, 2.0f, -1.5f, 3.0f },
                        new[] { float.PositiveInfinity, 2.0f, -1.5f, 3.0f }
                    },
                    new[] { 0f, float.NaN, 0.707106769f, 1.931105f, float.NaN, float.NaN, float.NaN, float.NaN } // expected output
                )
            );
        }

        /// <summary>
        /// Method that executes the test of the specified operator using the supplied input/expected output
        /// sequence pairs and verifies the generated outputs.
        /// </summary>
        /// <typeparam name="TInput">The type of the input stream.</typeparam>
        /// <typeparam name="TOutput">The type of the computed output stream.</typeparam>
        /// <param name="operator">The operator to apply to the input stream.</param>
        /// <param name="testInputOutput">A parameter list of the input sequence/expected output pairs.</param>
        private void RunTest<TInput, TOutput>(
            Func<IProducer<TInput>, DeliveryPolicy, IProducer<TOutput>> @operator,
            params (TInput[] input, TOutput[] output)[] testInputOutput)
        {
            // lists for capturing test output
            var outputs = new List<List<TOutput>>();

            // apply operator to input test sequences and capture the resulting output
            using (var p = Pipeline.Create())
            {
                foreach (var (inputSequence, _) in testInputOutput)
                {
                    var output = new List<TOutput>();
                    outputs.Add(output);
                    @operator(Generators.Sequence(p, inputSequence), null).Do(x => output.Add(x));
                }

                p.Run(null, true);
            }

            // verify actual output values against expected outputs
            for (int i = 0; i < testInputOutput.Length; i++)
            {
                CollectionAssert.AreEqual(testInputOutput[i].output, outputs[i]);
            }
        }
    }
}
