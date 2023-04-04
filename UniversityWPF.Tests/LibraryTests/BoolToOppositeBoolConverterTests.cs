using System.Globalization;
using UniversityWPF.Library.Converters;

namespace UniversityWPF.Tests.LibraryTests
{
    [TestClass]
    public class BoolToOppositeBoolConverterTests
	{
        [TestMethod]
        public void Convert_True_FalseExpected()
        {
			//Arrange
			var converter = new BoolToOppositeBoolConverter();
			bool expected = false;

			//Act
			bool actual = (bool)converter.Convert(true, typeof(bool), new object(), new CultureInfo("ru-RU"));

			//Assert
			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		public void Convert_False_TrueExpected()
		{
			//Arrange
			var converter = new BoolToOppositeBoolConverter();
			bool expected = true;

			//Act
			bool actual = (bool)converter.Convert(false, typeof(bool), new object(), new CultureInfo("ru-RU"));

			//Assert
			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Convert_ObjectIsNotBool_ArgumentExceptionExpected()
		{
			//Arrange
			var converter = new BoolToOppositeBoolConverter();
			bool expected = true;

			//Act
			bool actual = (bool)converter.Convert("some object", typeof(bool), new object(), new CultureInfo("ru-RU"));
		}

		[TestMethod]
		public void ConvertBack_True_FalseExpected()
		{
			//Arrange
			var converter = new BoolToOppositeBoolConverter();
			bool expected = false;

			//Act
			bool actual = (bool)converter.ConvertBack(true, typeof(bool), new object(), new CultureInfo("ru-RU"));

			//Assert
			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		public void ConvertBack_False_TrueExpected()
		{
			//Arrange
			var converter = new BoolToOppositeBoolConverter();
			bool expected = true;

			//Act
			bool actual = (bool)converter.ConvertBack(false, typeof(bool), new object(), new CultureInfo("ru-RU"));

			//Assert
			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ConvertBack_ObjectIsNotBool_ArgumentExceptionExpected()
		{
			//Arrange
			var converter = new BoolToOppositeBoolConverter();
			bool expected = true;

			//Act
			bool actual = (bool)converter.ConvertBack("some object", typeof(bool), new object(), new CultureInfo("ru-RU"));
		}
	}
}