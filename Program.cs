using System;
using System.Collections.Generic;
using System.Linq;
public class Vehicle
{
    public string RegistrationNumber { get; set; }
    public string Color { get; set; }
    public string Type { get; set; }
    public DateTime CheckInTime { get; set; }
}

public class ParkingLot
{
    public int SlotNumber { get; set; }
    public Vehicle Vehicle { get; set; }
    public bool IsAvailable => Vehicle == null;
}

public class ParkingSystem
{
    private List<ParkingLot> parkingLots;

    public ParkingSystem(int totalSlots)
    {
        parkingLots = new List<ParkingLot>();
        for (int i = 1; i <= totalSlots; i++)
        {
            parkingLots.Add(new ParkingLot { SlotNumber = i });
        }
    }

    public void ParkVehicle(string registrationNumber, string color, string type)
    {
        var countLot = parkingLots.Count;
        var availableLot = parkingLots.FirstOrDefault(lot => lot.IsAvailable);

        if (countLot != 0)
        {
            if (availableLot != null)
            {
                availableLot.Vehicle = new Vehicle
                {
                    RegistrationNumber = registrationNumber,
                    Color = color,
                    Type = type,
                    CheckInTime = DateTime.Now // waktu masuk
                };
                Console.WriteLine($"Allocated slot number: {availableLot.SlotNumber}");
            }
            else
            {
                Console.WriteLine("Sorry, parking lot is full");
            }
        }
        else
        {
            Console.WriteLine("There is no parking slot, please create first");
        }

    }

    public double CalculateParkingFee(DateTime checkInTime, DateTime checkOutTime, double hourlyRate)
    {
        var duration = checkOutTime - checkInTime;
        var totalHours = Math.Ceiling(duration.TotalHours); // pembulatan ke atas
        return totalHours * hourlyRate;
    }



    public void LeaveVehicle(int slotNumber, double hourlyRate)
    {
        var lot = parkingLots.FirstOrDefault(lot => lot.SlotNumber == slotNumber);

        if (lot != null && !lot.IsAvailable)
        {
            var checkOutTime = DateTime.Now;
            var fee = CalculateParkingFee(lot.Vehicle.CheckInTime, checkOutTime, hourlyRate);
            Console.WriteLine($"Slot number {slotNumber} is free. Parking fee: {fee:C}");
            lot.Vehicle = null;
        }
        else
        {
            Console.WriteLine($"Slot number {slotNumber} is already free or does not exist");
        }
    }

    public void GetStatus()
    {
        Console.WriteLine("Slot\tNo.\tType\tRegistration No\tColour");
        foreach (var lot in parkingLots.Where(lot => !lot.IsAvailable))
        {
            Console.WriteLine($"{lot.SlotNumber}\t{lot.Vehicle.RegistrationNumber}\t{lot.Vehicle.Type}\t{lot.Vehicle.Color}");
        }
    }

    public void FilledLot()
    {
        var countLot = parkingLots.Count;
        var availableLot = parkingLots.Count(lot => lot.IsAvailable);

        if (countLot != 0)
        {
            double filledLot = countLot - availableLot;
            Console.WriteLine($"{filledLot} lot filled");
        }
        else
        {
            Console.WriteLine("There is no parking slot, please create first");
        }

    }

    public void AvailableLot()
    {
        var countLot = parkingLots.Count;
        var availableLot = parkingLots.Count(lot => lot.IsAvailable);

        if (countLot != 0)
        {
            Console.WriteLine($"{availableLot} lot is available");
        }
        else
        {
            Console.WriteLine("There is no parking slot, please create first");
        }

    }

    public void TypeOfVehicle(string vehicleType)
    {
        var findByType = parkingLots.Where(lot => !lot.IsAvailable && lot.Vehicle.Type == vehicleType).ToList();

        if (findByType.Count > 0)
        {
            Console.WriteLine(findByType.Count);
        }
        else
        {
            Console.WriteLine($"No vehicles of type '{vehicleType}' found");
        }
    }

    public void colorOfVehicle(string vehicleColor)
    {
        var findByColor = parkingLots.Where(lot => !lot.IsAvailable && lot.Vehicle.Color == vehicleColor).ToList();

        if (findByColor.Count > 0)
        {
            Console.WriteLine(findByColor.Count);
        }
        else
        {
            Console.WriteLine($"No vehicles of color '{vehicleColor}' found");
        }
    }

    public void oodPlate()
    {
        var oodPlateNumbers = new List<string>();
        var findAll = parkingLots.Where(lot => !lot.IsAvailable).ToList();

        if (findAll.Count > 0)
        {
            for (int i = 0; i < findAll.Count; i++)
            {
                var regNumber = findAll[i].Vehicle.RegistrationNumber;
                var splitReg = regNumber.Split('-');
                var getNumber = Int32.Parse(splitReg[1]);

                if (getNumber % 2 != 0)
                {
                    oodPlateNumbers.Add(regNumber);
                }
            }

            if (oodPlateNumbers.Count > 0)
            {
                for (int i = 0; i < oodPlateNumbers.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.Write(oodPlateNumbers[i]);
                    }
                    else
                    {
                        Console.Write($", {oodPlateNumbers[i]}");
                    }

                }
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Ood Plate Not Found");
            }
        }
        else
        {
            Console.WriteLine("No vehicles found");
        }
    }

    public void evenPlate()
    {
        var evenPlateNumbers = new List<string>();
        var findAll = parkingLots.Where(lot => !lot.IsAvailable).ToList();

        if (findAll.Count > 0)
        {
            for (int i = 0; i < findAll.Count; i++)
            {
                var regNumber = findAll[i].Vehicle.RegistrationNumber;
                var splitReg = regNumber.Split('-');
                var getNumber = Int32.Parse(splitReg[1]);

                if (getNumber % 2 == 0)
                {
                    evenPlateNumbers.Add(regNumber);
                }
            }

            if (evenPlateNumbers.Count > 0)
            {
                for (int i = 0; i < evenPlateNumbers.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.Write(evenPlateNumbers[i]);
                    }
                    else
                    {
                        Console.Write($", {evenPlateNumbers[i]}");
                    }

                }
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Even Plate Not Found");
            }
        }
        else
        {
            Console.WriteLine("No vehicles found");
        }

    }

    public void regWithColour(string color)
    {
        var filteredPlateNumbers = new List<string>();
        var findByColour = parkingLots.Where(lot => !lot.IsAvailable && lot.Vehicle.Color == color).ToList();

        if (findByColour.Count > 0)
        {
            for (int i = 0; i < findByColour.Count; i++)
            {
                var regNumber = findByColour[i].Vehicle.RegistrationNumber;

                filteredPlateNumbers.Add(regNumber);
            }

            if (filteredPlateNumbers.Count > 0)
            {
                for (int i = 0; i < filteredPlateNumbers.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.Write(filteredPlateNumbers[i]);
                    }
                    else
                    {
                        Console.Write($", {filteredPlateNumbers[i]}");
                    }

                }
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine($"vehicle with '{color}' colour not found");
            }
        }
        else
        {
            Console.WriteLine($"vehicle with '{color}' colour not found");
        }
    }
    public void slotWithColour(string color)
    {
        var filteredSlotNumbers = new List<int>();
        var findByColour = parkingLots.Where(lot => !lot.IsAvailable && lot.Vehicle.Color == color).ToList();

        if (findByColour.Count > 0)
        {
            for (int i = 0; i < findByColour.Count; i++)
            {
                var slotNumber = findByColour[i].SlotNumber;

                filteredSlotNumbers.Add(slotNumber);
            }

            if (filteredSlotNumbers.Count > 0)
            {
                for (int i = 0; i < filteredSlotNumbers.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.Write(filteredSlotNumbers[i]);
                    }
                    else
                    {
                        Console.Write($", {filteredSlotNumbers[i]}");
                    }

                }
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine($"vehicle with '{color}' colour not found");
            }
        }
        else
        {
            Console.WriteLine($"vehicle with '{color}' colour not found");
        }
    }
    public void slotWithRegNumber(string regNumber)
    {
        var filteredSlotNumbers = new List<int>();
        var findByRegNumber = parkingLots.Where(lot => !lot.IsAvailable && lot.Vehicle.RegistrationNumber == regNumber).ToList();

        if (findByRegNumber.Count > 0)
        {
            for (int i = 0; i < findByRegNumber.Count; i++)
            {
                var slotNumber = findByRegNumber[i].SlotNumber;

                filteredSlotNumbers.Add(slotNumber);
            }

            if (filteredSlotNumbers.Count > 0)
            {
                for (int i = 0; i < filteredSlotNumbers.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.Write(filteredSlotNumbers[i]);
                    }
                    else
                    {
                        Console.Write($", {filteredSlotNumbers[i]}");
                    }

                }
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }
        else
        {
            Console.WriteLine("Not found");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var parkingSystem = new ParkingSystem(0); // jumlah lot available (default)
        double hourlyRate = 3000; // biaya parkir

        while (true)
        {
            var input = Console.ReadLine();
            var commands = input.Split(' ');

            switch (commands[0])
            {
                case "create_parking_lot":
                    parkingSystem = new ParkingSystem(int.Parse(commands[1]));
                    Console.WriteLine($"Created a parking lot with {commands[1]} slots");
                    break;
                case "park":
                    parkingSystem.ParkVehicle(commands[1], commands[2], commands[3]);
                    break;
                case "leave":
                    parkingSystem.LeaveVehicle(int.Parse(commands[1]), hourlyRate);
                    break;
                case "status":
                    parkingSystem.GetStatus();
                    break;
                case "filled_lot":
                    parkingSystem.FilledLot();
                    break;
                case "available_lot":
                    parkingSystem.AvailableLot();
                    break;
                case "type_of_vehicles":
                    parkingSystem.TypeOfVehicle(commands[1]);
                    break;
                case "colour_of_vehicles":
                    parkingSystem.colorOfVehicle(commands[1]);
                    break;
                case "registration_numbers_for_vehicles_with_ood_plate":
                    parkingSystem.oodPlate();
                    break;
                case "registration_numbers_for_vehicles_with_even_plate":
                    parkingSystem.evenPlate();
                    break;
                case "registration_numbers_for_vehicles_with_colour":
                    parkingSystem.regWithColour(commands[1]);
                    break;
                case "slot_numbers_for_vehicles_with_colour":
                    parkingSystem.slotWithColour(commands[1]);
                    break;
                case "slot_number_for_registration_number":
                    parkingSystem.slotWithRegNumber(commands[1]);
                    break;
                default:
                    Console.WriteLine("Invalid command, please try another.");
                    break;
                case "exit":
                    return;
            }
        }
    }
}
