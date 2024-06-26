﻿using FluentAssertions;
using Xunit;

namespace SlugEnt.FluentResults.Test
{
    public class SuccessStringifyTests
    {
        [Fact]
        public void EmptySuccessToString_OnlyType()
        {
            // Act
            var error = new Success("My first success");

            // Assert
            error.ToString().Should().Be("Success with Message='My first success'");
        }
    }
}
