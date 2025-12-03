using System;
using Domain.Interfaces.Repository;

namespace Domain.UseCases.RegisterUser.Services;

/// <summary>
/// Domain service responsible for generating unique user codes.
/// Can generate the next code based on repository rules or set a manual code.
/// </summary>
/// <remarks>
/// Author: Ittikorn Sopawan
/// </remarks>
public interface IUserCodeGeneratorDomain
{
    /// <summary>
    /// Generates a unique user code and sets it internally.
    /// </summary>
    /// <param name="isActive">Indicates whether the user is active (default: true).</param>
    /// <returns>The generated user code as string.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the repository returns invalid user number.</exception>
    Task<string> GenerateAsync(bool isActive = true);

    /// <summary>
    /// Sets a manual user code (override the internally stored code).
    /// </summary>
    /// <param name="manualCode">The user code to set manually.</param>
    /// <returns>The same domain service instance for chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when the manual code is null or empty.</exception>
    IUserCodeGeneratorDomain Set(string manualCode);

    /// <summary>
    /// Marks the user code as active.
    /// </summary>
    /// <returns>The same domain service instance for chaining.</returns>
    IUserCodeGeneratorDomain SetActivate(bool isActive);

    /// <summary>
    /// Returns the current user code (latest generated or manually set).
    /// </summary>
    string CurrentCode { get; }

    /// <summary>
    /// Sets the current user code.
    /// </summary>
    /// <param name="code">
    /// The user code to set. Must follow the format 'XXXXXX-0/1', where the last digit indicates active (1) or inactive (0) status.
    /// </param>
    /// <remarks>
    /// This will overwrite any previously generated or set code.
    /// Implementations should validate the input and throw <see cref="ArgumentException"/> if the code is null, empty, or in an invalid format.
    /// </remarks>
    /// <returns>
    /// Returns the same <see cref="IUserCodeGeneratorDomain"/> instance to allow method chaining.
    /// </returns>
    IUserCodeGeneratorDomain SetCode(string code);
}

public class UserCodeGeneratorDomain : IUserCodeGeneratorDomain
{
    private readonly IUserQueryRepository _repository;
    private string _code = string.Empty;

    /// <summary>
    /// Initializes a new instance of <see cref="UserCodeGeneratorDomain"/>.
    /// </summary>
    /// <param name="repository">The repository used to get existing users.</param>
    /// <exception cref="ArgumentNullException">Thrown when repository is null.</exception>
    public UserCodeGeneratorDomain(IUserQueryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// Generates a unique user code and sets it internally.
    /// </summary>
    /// <param name="isActive">Indicates whether the user is active (default: true).</param>
    /// <returns>The generated user code as string.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the repository returns invalid user number.</exception>
    public async Task<string> GenerateAsync(bool isActive = true)
    {
        var userNo = await _repository.GetExistUser();

        if (userNo < 0)
            throw new InvalidOperationException("UserNo cannot be negative.");

        var nextNo = userNo + 1;
        var numberPart = nextNo.ToString("D6");
        var statusPart = isActive ? "1" : "0";

        _code = $"{numberPart}-{statusPart}";
        return _code;
    }

    /// <summary>
    /// Sets a manual user code (override the internally stored code).
    /// </summary>
    /// <param name="manualCode">The user code to set manually.</param>
    /// <returns>The same domain service instance for chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when the manual code is null or empty.</exception>
    public IUserCodeGeneratorDomain Set(string manualCode)
    {
        if (string.IsNullOrWhiteSpace(manualCode))
            throw new ArgumentException("User code cannot be null or empty.", nameof(manualCode));

        _code = manualCode.Trim();
        return this;
    }

    /// <summary>
    /// Sets the active/inactive status of the current user code.
    /// </summary>
    /// <param name="isActive">
    /// True to mark as active ("-1"), false to mark as inactive ("-0").
    /// </param>
    /// <remarks>
    /// Only updates the status part if it differs from the current status.
    /// Throws <see cref="InvalidOperationException"/> if the current code is null or has invalid format.
    /// </remarks>
    /// <returns>
    /// Returns the same <see cref="IUserCodeGeneratorDomain"/> instance for method chaining.
    /// </returns>
    public IUserCodeGeneratorDomain SetActivate(bool isActive)
    {
        if (string.IsNullOrWhiteSpace(_code))
            throw new InvalidOperationException("Cannot update status before generating or setting a code.");

        var parts = _code.Split('-');
        if (parts.Length != 2)
            throw new InvalidOperationException("User code format invalid. Expected 'XXXXXX-0/1'.");

        var currentStatus = parts[1];
        var newStatus = isActive ? "1" : "0";

        if (currentStatus != newStatus)
        {
            parts[1] = newStatus;
            _code = $"{parts[0]}-{parts[1]}";
        }

        return this;
    }

    /// <summary>
    /// Sets the current user code manually.
    /// </summary>
    /// <param name="manualCode">The user code to set (must follow 'XXXXXX-0/1' format).</param>
    /// <remarks>
    /// This will overwrite any previously generated or set code.
    /// Throws <see cref="ArgumentException"/> if <paramref name="manualCode"/> is null, empty, or whitespace.
    /// </remarks>
    /// <returns>
    /// Returns the same <see cref="IUserCodeGeneratorDomain"/> instance for method chaining.
    /// </returns>
    public IUserCodeGeneratorDomain SetCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("User code cannot be null or empty.", nameof(code));

        // Optionally, validate format "XXXXXX-0/1"
        var parts = code.Split('-');
        if (parts.Length != 2 || (parts[1] != "0" && parts[1] != "1"))
            throw new ArgumentException("User code format invalid. Expected 'XXXXXX-0/1'.", nameof(code));

        _code = code.Trim();
        return this;
    }

    /// <summary>
    /// Returns the current user code (latest generated or manually set).
    /// </summary>
    public string CurrentCode => _code;
}