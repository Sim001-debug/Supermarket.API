using System.ComponentModel;

namespace Supermarket.API.Models
{
    public enum EUnitOfMeasurement
    {
        [Description("Unity")]
        Unity = 1,

        [Description("Liter")]
        Liter = 2,

        [Description("Kilogram")]
        Kilogram = 3,
    }
}
