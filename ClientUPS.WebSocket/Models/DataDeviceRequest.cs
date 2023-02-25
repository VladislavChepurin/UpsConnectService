﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ClientUPS.WebSocket.Models;

[Table("Devices")]

public class DataDeviceRequest
{
   // public Guid Id { get; set; } = Guid.NewGuid();
    public string? SerialNumber { get; set; }
    public string? NameDevice { get; set; }
    public int StatusCode { get; set; }
    public double InputVoltage { get; set; }
    public double OutputVoltage { get; set; }
    public double InputСurrent { get; set; }
    public double OutputCurrent { get; set; }
    public double BatteryCharge { get; set; }
    public bool Bypass { get; set; }
    public bool GeneralError { get; set; }
    public bool BatteryError { get; set; }
}