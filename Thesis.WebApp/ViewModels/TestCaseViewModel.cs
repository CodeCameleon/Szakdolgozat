using Shared.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestResults.Entities;
using TestResults.Services.Interfaces;

namespace Thesis.WebApp.ViewModels;

/// <summary>
/// Egy tesztesetet ábrázoló nézetmodell.
/// </summary>
public class TestCaseViewModel
    : IValidatableObject
{
    /// <summary>
    /// A teszteset azonosítója.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A teszteset engedélyezve van-e.
    /// </summary>
    [DisplayName("Engedélyezve")]
    public bool Enabled { get; set; }

    /// <summary>
    /// A teszteset bemenete.
    /// </summary>
    [DisplayName("Bemenet")]
    [Required(ErrorMessage = "A bemenet megadása kötelező.")]
    public required string Input { get; set; }

    /// <summary>
    /// A teszteset nézetmodel explicit átalakítása teszteset entitássá.
    /// </summary>
    /// <param name="testCaseViewModel">Az átalakítandó teszteset nézetmodell.</param>
    public static explicit operator TestCase(TestCaseViewModel testCaseViewModel)
    {
        return new TestCase
        {
            Id = testCaseViewModel.Id,
            Enabled = testCaseViewModel.Enabled,
            Input = testCaseViewModel.Input
        };
    }

    /// <summary>
    /// A teszteset entitás explicit átalakítása teszteset nézetmodellé.
    /// </summary>
    /// <param name="testCase">Az átalakítandó teszteset entitás.</param>
    public static explicit operator TestCaseViewModel(TestCase testCase)
    {
        return new TestCaseViewModel
        {
            Id = testCase.Id,
            Enabled = testCase.Enabled,
            Input = testCase.Input
        };
    }

    /// <summary>
    /// Leellenőrzi, hogy a nézetmodell megfelelően van-e kitöltve.
    /// </summary>
    /// <param name="validationContext">A validálás kontextusa.</param>
    /// <returns>A validációs hibák.</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        ITestCaseService testCaseService = validationContext.GetRequiredService<ITestCaseService>();

        if (testCaseService.ExistsAsync(Input).Result)
        {
            yield return new ValidationResult(ErrorMessages.TestCaseInputExists, [nameof(Input)]);
        }
    }
}
