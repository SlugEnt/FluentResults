﻿using System;
using System.Threading.Tasks;
using SlugEnt.FluentResults.Samples.WebController;
using Hangfire;
using SlugEnt.FluentResults.Samples.WebController;

namespace SlugEnt.FluentResults.Samples.HangfireJobs
{
    public class CustomerJob
    {
        public static void AddOrUpdateJob() { RecurringJob.AddOrUpdate(() => Dispatch(), Cron.Daily); }


        public static Task<ResultDto> Dispatch()
        {
            // some logic
            Console.WriteLine("Fire-and-forget!");

            // Use an custom ResultDto class so that the serialization is in your control
            // Transform the FluentResult Result object to an custom ResultDto object as last as possible.
            return Task.FromResult(Result.Ok().ToResultDto());
        }
    }
}