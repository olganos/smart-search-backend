﻿namespace Core;

public enum ExactMatchRatio
{
    Value = 10,
}

public enum BuildingWeight
{
    ShortCut = 7,
    Name = 9,
    Description = 5,
}

public enum LockWeight
{
    Type = 3,
    Name = 10,
    SerialNumber = 8,
    Floor = 6,
    RoomNumber = 6,
    Description = 6,
    BuildingName = 8,
    BuildingShortcut = 5
}

public enum GroupWeight
{
    Name = 9,
    Description = 5,
}

public enum MediumWeight
{
    Type = 3,
    Owner = 10,
    SerialNumber = 8,
    Description = 6,
    GroupName = 8
}
