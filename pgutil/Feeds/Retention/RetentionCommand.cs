﻿using ConsoleMan;
using Inedo.ProGet;

namespace PgUtil;

internal sealed partial class Program
{
    private sealed partial class FeedsCommand
    {
        private sealed partial class RetentionCommand : IConsoleCommandContainer
        {
            public static string Name => "retention";
            public static string Description => "List or modify feed retention rules";

            public static void Configure(ICommandBuilder builder)
            {
                builder.WithOption<FeedOption>()
                    .WithCommand<ListCommand>()
                    .WithCommand<DeleteCommand>()
                    .WithCommand<CreateCommand>()
                    .WithCommand<UpdateCommand>();
            }

            private static class RetentionOptions
            {
                public static ICommandBuilder Configure(ICommandBuilder builder)
                {
                    return builder.WithOption<DeleteCached>()
                        .WithOption<DeletePackageIds>()
                        .WithOption<DeletePrereleaseVersions>()
                        .WithOption<DeleteVersions>()
                        .WithOption<KeepConsumedWithinDays>()
                        .WithOption<KeepIfActivelyConsumed>()
                        .WithOption<KeepPackageIds>()
                        .WithOption<KeepPackageUsageRemovedDays>()
                        .WithOption<KeepUsedWithinDays>()
                        .WithOption<KeepVersionsCount>()
                        .WithOption<KeepVersions>()
                        .WithOption<SizeExclusive>()
                        .WithOption<SizeTrigger>()
                        .WithOption<TriggerDownloadCount>();
                }

                public static void Assign(CommandContext context, RetentionRule rule)
                {
                    int c = 0;
                    c += p<DeleteCached>();
                    c += p<DeletePackageIds>();
                    c += p<DeletePrereleaseVersions>();
                    c += p<DeleteVersions>();
                    c += p<KeepConsumedWithinDays>();
                    c += p<KeepIfActivelyConsumed>();
                    c += p<KeepPackageIds>();
                    c += p<KeepPackageUsageRemovedDays>();
                    c += p<KeepUsedWithinDays>();
                    c += p<KeepVersionsCount>();
                    c += p<KeepVersions>();
                    c += p<SizeExclusive>();
                    c += p<SizeTrigger>();
                    c += p<TriggerDownloadCount>();

                    if (c == 0)
                        throw new PgUtilException("At least one retention option must be specified.");

                    int p<TOption>() where TOption : IRetentionOption
                    {
                        if (!context.TryGetOption<TOption>(out var value))
                            return 0;

                        TOption.Assign(rule, value);
                        return 1;
                    }
                }

                private sealed class DeleteCached : IRetentionOption
                {
                    public static string Name => "--deleteCached";
                    public static string Description => "Consider only cached packages or all packages";
                    public static string[] ValidValues => ["true", "false"];

                    public static void Assign(RetentionRule rule, string value) => rule.DeleteCached = bool.Parse(value);
                }

                private sealed class DeletePackageIds : IRetentionOption
                {
                    public static string Name => "--deletePackageIds";
                    public static string Description => "Comma separated list of packages subject for deletion";

                    public static void Assign(RetentionRule rule, string value) => rule.DeletePackageIds = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                }

                private sealed class DeletePrereleaseVersions : IRetentionOption
                {
                    public static string Name => "--deletePrereleaseVersions";
                    public static string Description => "Consider only prerelease versions or all versions";
                    public static string[] ValidValues => ["true", "false"];

                    public static void Assign(RetentionRule rule, string value) => rule.DeletePrereleaseVersions = bool.Parse(value);
                }

                private sealed class DeleteVersions : IRetentionOption
                {
                    public static string Name => "--deleteVersions";
                    public static string Description => "Comma separated list of versions subject for deletion";

                    public static void Assign(RetentionRule rule, string value) => rule.DeleteVersions = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                }

                private sealed class KeepConsumedWithinDays : IRetentionOption
                {
                    public static string Name => "--keepConsumedWithinDays";
                    public static string Description => "Package must not have been used within this many days to qualifier for deletion";

                    public static void Assign(RetentionRule rule, string value) => rule.KeepConsumedWithinDays = int.Parse(value);
                }

                private sealed class KeepIfActivelyConsumed : IRetentionOption
                {
                    public static string Name => "--keepIfActivelyConsumed";
                    public static string Description => "Keep if actively consumed";
                    public static string[] ValidValues => ["true", "false"];

                    public static void Assign(RetentionRule rule, string value) => rule.KeepIfActivelyConsumed = bool.Parse(value);
                }

                private sealed class KeepPackageIds : IRetentionOption
                {
                    public static string Name => "--keepPackageIds";
                    public static string Description => "Comma separated list of packages exempt from deletion";

                    public static void Assign(RetentionRule rule, string value) => rule.KeepPackageIds = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                }

                private sealed class KeepPackageUsageRemovedDays : IRetentionOption
                {
                    public static string Name => "--keepPackageUsageRemovedDays";
                    public static string Description => "Keep package usage removed days";

                    public static void Assign(RetentionRule rule, string value) => rule.KeepPackageUsageRemovedDays = int.Parse(value);
                }

                private sealed class KeepUsedWithinDays : IRetentionOption
                {
                    public static string Name => "--keepUsedWithinDays";
                    public static string Description => "Package must not have been used within this many days to qualifier for deletion";

                    public static void Assign(RetentionRule rule, string value) => rule.KeepUsedWithinDays = int.Parse(value);
                }

                private sealed class KeepVersionsCount : IRetentionOption
                {
                    public static string Name => "--keepVersionsCount";
                    public static string Description => "Always keep this many of the latest versions of the package";

                    public static void Assign(RetentionRule rule, string value) => rule.KeepVersionsCount = int.Parse(value);
                }

                private sealed class KeepVersions : IRetentionOption
                {
                    public static string Name => "--keepVersions";
                    public static string Description => "Comma separated list of versions exempt from deletion";

                    public static void Assign(RetentionRule rule, string value) => rule.KeepVersions = value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                }

                private sealed class SizeExclusive : IRetentionOption
                {
                    public static string Name => "--sizeExclusive";
                    public static string Description => "Specifies if the sizeTrigger value is an exclusive boundary";
                    public static string[] ValidValues => ["true", "false"];

                    public static void Assign(RetentionRule rule, string value) => rule.SizeExclusive = bool.Parse(value);
                }

                private sealed class SizeTrigger : IRetentionOption
                {
                    public static string Name => "--sizeTrigger";
                    public static string Description => "Minimum size (in kb) of feed required to trigger retention run";

                    public static void Assign(RetentionRule rule, string value) => rule.SizeTriggerKb = long.Parse(value);
                }

                private sealed class TriggerDownloadCount : IRetentionOption
                {
                    public static string Name => "--triggerDownloadCount";
                    public static string Description => "Trigger download count";

                    public static void Assign(RetentionRule rule, string value) => rule.TriggerDownloadCount = int.Parse(value);
                }

                private interface IRetentionOption : IConsoleOption
                {
                    static bool IConsoleOption.Required => false;

                    static abstract void Assign(RetentionRule rule, string value);
                }
            }
        }
    }
}