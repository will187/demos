﻿using Domain.Common.Interfaces;

namespace Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime NowUtc => DateTime.UtcNow;
}
