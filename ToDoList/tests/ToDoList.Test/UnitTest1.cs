namespace ToDoList.Test;

public class UnitTest1
{
    [Fact]
    public void Test1() //Get_AllItems_ReturnsAllItems
    {
        //arrange
        var calculator = new Calculator();

        //act
        var result = Calculator.Divide(10, 5);

        //assert
        Assert.Equal(2, result); //expected, real

    }

    [Fact]
    public void Test2()
    {
        //arrange
        var calculator = new Calculator();

        //act
        //var divideAction = () => Calculator.Divide(10, 0);
        var result = Calculator.Divide(10, 0);
        //assert
        Assert.Equal(float.PositiveInfinity, result); //Assert.Throws<DivideByZeroException>(divideAction); //expected, real
        //Assert.Throws<DivideByZeroException>()

        //Assert.Throws<DivideByZeroException>(divideAction); //expected, real

    }
}

public class Calculator
{
    public static float Divide(float dividend, float divisor) => dividend / divisor;


}

/*public class Calculator
{
    public int Divide(int dividend, int divisor) => dividend / divisor;


}*/
