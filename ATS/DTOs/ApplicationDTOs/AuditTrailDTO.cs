namespace ATS.DTOs.ApplicationDTOs;

public record AuditTrailDTO(
    string FromStage,
    string ToStage,
    string ChangedBy,
    DateTime ChangedAt,
    string? Comment
);