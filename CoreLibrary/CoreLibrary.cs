
namespace CoreLibrary;

public class ScaleConversion
{
    public enum Metrics
    {
        Feet,
        Inches,
        Metres,
        Centimetres,
        Millimetres
    }

    private double FeetToInches = 12.0;
    private double InchesToMillimetres = 25.4;
    private double MetreToMilliMetre = 1000;
    private double CentimetresToMilliMetre = 10;

    public double ConvertToScale(double measurement, double scale, Metrics metrics, int precision)
    {
        double result = 0.0;

        switch (metrics)
        {
            case Metrics.Feet: result = ((measurement * FeetToInches) * InchesToMillimetres) / scale; break;
            case Metrics.Inches: result = ((measurement * InchesToMillimetres) / scale); break;
            case Metrics.Metres: result = ((measurement * MetreToMilliMetre) / scale); break;
            case Metrics.Centimetres: result = ((measurement * CentimetresToMilliMetre) / scale); break;
            case Metrics.Millimetres: result = ((measurement) / scale); break;
            default: break;
        }

        return Math.Round(result, precision);
    }
}

public class Common
{

}
