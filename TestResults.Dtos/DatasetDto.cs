namespace TestResults.Dtos;

/// <summary>
/// Egy adathalmazt ábrázoló adatátmeneti objektum.
/// </summary>
public class DatasetDto
{
    /// <summary>
    /// Az adathalmaz címkéje.
    /// </summary>
    public required string Label { get; set; }

    /// <summary>
    /// Az adathalmaz elemei.
    /// </summary>
    public required List<DataDto> DataList { get; set; }

    /// <summary>
    /// Az adathalmaz szegélyszíne.
    /// </summary>
    public required string BorderColor { get; set; }

    /// <summary>
    /// Az adathalmaz háttérszíne.
    /// </summary>
    public required string BackgroundColor { get; set; }

    /// <summary>
    /// Az adathalmaz típusa.
    /// </summary>
    public required string Type { get; set; }
}
