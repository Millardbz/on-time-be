namespace on_time_be.WebUI.Controllers;

public class GeneralRequestModel
{
    public string? Type { get; set; } // Could be the type of command or query
    public dynamic? Payload { get; set; } // The data for the command or query
}

