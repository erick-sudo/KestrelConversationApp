global using Core.Entities;
global using Core.DTOs.Company;
global using Core.DTOs.Employee;
global using Core.Interfaces.Repositories;
global using Core.Interfaces.Services;
global using Core.Utilities.Constants;
global using Core.Utilities.Results;
global using Core.Identity;

global using Infrastructure.Data;
global using Infrastructure.ExtensionMethods;

global using Integration.Base;
global using Integration.Fixtures;
global using Integration.Harness;

global using System.Security.Claims;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using NSubstitute;
global using Xunit;
global using FluentAssertions;