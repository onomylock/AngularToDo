namespace Common.Common.Models;

public static class Localize
{
    #region SignalRHub

    public static string SignalRHubJsonWebTokenGroupKey => "#GroupKey:JsonWebToken<{0}>";

    #endregion

    public static class Keys
    {
        public static class Error
        {
            #region HttpClient

            public static string ResponseStatusCodeUnsuccessful => "#UI_ResponseStatusCodeUnsuccessful";
            public static string RequestTimedOut => "#UI_RequestTimedOut";

            #endregion

            #region Exception

            public const string UnhandledExceptionContactSystemAdministrator =
                "#UI_UnhandledExceptionContactSystemAdministrator";

            public const string HandledExceptionContactSystemAdministrator =
                "#UI_HandledExceptionContactSystemAdministrator";

            #endregion

            #region Generic

            public const string DbContextNoTransactionInProgress = "#UI_DbContextNoTransactionInProgressException";

            public const string DbContextAnotherTransactionInProgress =
                "#UI_DbContextAnotherTransactionInProgressException";

            #endregion

            #region DrillAutomation

            public const string DrillingJointExtensionModeOperationNotSupported =
                "#UI_DrillingJointExtensionModeOperationNotSupported";

            public const string CanDeviceResponseVerificationFailed = "#UI_CanDeviceResponseVerificationFailed";

            public const string CanResponseVerificationFailedPwmChannelOutOfBounds =
                "#UI_CanResponseVerificationFailedPwmChannelOutOfBounds";

            public const string CanResponseVerificationFailedPwmChannelGroupOutOfBounds =
                "#UI_CanResponseVerificationFailedPwmChannelGroupOutOfBounds";

            public const string CanResponseVerificationFailedPwmCoilOutOfBounds =
                "#UI_CanResponseVerificationFailedPwmCoilOutOfBounds";

            public const string CanResponseVerificationFailedHzP1OutOfBounds =
                "#UI_CanResponseVerificationFailedHzP1OutOfBounds";

            public const string CanResponseVerificationFailedHzP0OutOfBounds =
                "#UI_CanResponseVerificationFailedHzP0OutOfBounds";

            public const string CanResponseVerificationFailedValueOutOfBounds =
                "#UI_CanResponseVerificationFailedValueOutOfBounds";

            public const string CanResponseVerificationFailedParam0Param1OutOfBounds =
                "#UI_CanResponseVerificationFailedParam0Param1OutOfBounds";

            public const string CanResponseVerificationFailedUnknown = "#UI_CanResponseVerificationFailedUnknown";
            public const string RemoteControlNotPermitted = "#UI_COMMON.EXCEPTION.REMOTECONTROLNOTPERMITTEDEXCEPTION";

            public const string UserСanNotSelectHoleWhileAutoHoleSelectionIsEnabled =
                "#UI_UserСanNotSelectHoleWhileAutoHoleSelectionIsEnabled";

            public const string UserСanNotSelectDrillBlockWhileDrillingAlgorithmIsRunning =
                "#UI_UserСanNotSelectDrillBlockWhileDrillingAlgorithmIsRunning";

            public const string OperationNotSupportedForAppModeDrillManager =
                "#UI_OperationNotSupportedExceptionForAppModeDrillManager";

            public const string DrillingRequiresAlignmentStatusToStart = "#UI_DrillingRequiresAlignmentStatusToStart";

            #endregion
        }

        public static class Warning
        {
            public static string XssVulnerable => "#UI_XssVulnerable";
        }

        public static class Notifications
        {
            public const string Drilling = "#UI_Drilling";
            public const string DrillingJointExtension = "#UI_DrillingJointExtension";
            public const string DrillingRodInWellBasedOnAirPressure = "#UI_DrillingRodInWell";
            public const string DrillingSensorsValuesOutOfSafeRange = "#UI_DrillingSensorsValuesOutOfSafeRange";

            public const string DrillingJointExtensionUserIncreaseVerify =
                "#UI_DrillingJointExtensionUserIncreaseVerify";

            public const string DrillingJointExtensionUserDecreaseVerify =
                "#UI_DrillingJointExtensionUserDecreaseVerify";

            public const string DrillingJointExtensionAutomaticIncreaseDecreaseAsk =
                "#UI_DrillingJointExtensionAutomaticIncreaseDecreaseAsk";

            public const string DrillingJointExtensionFailed = "#UI_DrillingJointExtensionFailed";
            public const string DrillingJointExtensionSuccess = "#UI_DrillingJointExtensionSuccess";
            public const string DrillingJointExtensionInProgress = "#UI_DrillingJointExtensionInProgress";

            public const string DrillingJointExtensionAirPressureHigherThanRequired =
                "#UI_DrillingJointExtensionAirPressureHigherThanRequired";

            public const string DrillingJointExtensionNotDrilling = "#UI_DrillingJointExtensionNotDrilling";

            public const string DrillingJointExtensionDrillingDepthLessThanRequired =
                "#UI_DrillingJointExtensionDrillingDepthLessThanRequired";

            public const string DrillingJointExtensionInitialRodRemovalNotPermitted =
                "#UI_DrillingJointExtensionInitialRodRemovalNotPermitted";

            public const string Yes = "#UI_Yes";
            public const string No = "#UI_No";
            public const string Increase = "#UI_Increase";
            public const string Decrease = "#UI_Decrease";
            public const string AlgorithmCompletedCorrectly = "#UI_AlgorithmCompletedCorrectly";
            public const string AlgorithmHaltedFaulty = "#UI_AlgorithmHaltedFaulty";
            public const string DrillAlgorithm = "#UI_DrillAlgorithm";
            public const string DrillAlgorithmAligningRequired = "#UI_DrillAlgorithmAligningRequired";

            public const string DrillAlgorithmAutomaticDrillStatusBeforeDrilling =
                "#UI_DrillAlgorithmAutomaticDrillStatusBeforeDrilling";

            public const string DrillAlgorithmAutomaticDrillStatusInitialSectionDrilling =
                "#UI_DrillAlgorithmAutomaticDrillStatusInitialSectionDrilling";

            public const string DrillAlgorithmAutomaticDrillStatusMainSectionDrilling =
                "#UI_DrillAlgorithmAutomaticDrillStatusMainSectionDrilling";

            public const string DrillAlgorithmAutomaticDrillStatusLastSectionDrilling =
                "#UI_DrillAlgorithmAutomaticDrillStatusLastSectionDrilling";

            public const string DrillAlgorithmAutomaticDrillStatusAfterDrilling =
                "#UI_DrillAlgorithmAutomaticDrillStatusAfterDrilling";

            public const string DrillAlgorithmAutomaticDrillStatusFinalization =
                "#UI_DrillAlgorithmAutomaticDrillStatusFinalization";

            public const string DrillAlgorithmAutomaticDrillStatusWaitDrillJointExtension =
                "#UI_DrillAlgorithmAutomaticDrillStatusWaitDrillJointExtension";

            public const string DrillAlgorithmAutomaticDrillStatusMoveBeforeDrillJointExtension =
                "#UI_DrillAlgorithmAutomaticDrillStatusMoveBeforeDrillJointExtension";

            public const string AlignAlgorithm = "#UI_AlignAlgorithm";

            public const string AlignAlgorithmAutomaticAlignStatusInitialMoveJackFront =
                "#UI_AlignAlgorithmAutomaticAlignStatusInitialMoveJackFront";

            public const string AlignAlgorithmAutomaticAlignStatusInitialMoveJackFrontLeft =
                "#UI_AlignAlgorithmAutomaticAlignStatusInitialMoveJackFrontLeft";

            public const string AlignAlgorithmAutomaticAlignStatusInitialMoveJackFrontRight =
                "#UI_AlignAlgorithmAutomaticAlignStatusInitialMoveJackFrontRight";

            public const string AlignAlgorithmAutomaticAlignStatusInitialMoveJackBackLeft =
                "#UI_AlignAlgorithmAutomaticAlignStatusInitialMoveJackBackLeft";

            public const string AlignAlgorithmAutomaticAlignStatusInitialMoveJackBackRight =
                "#UI_AlignAlgorithmAutomaticAlignStatusInitialMoveJackBackRight";

            public const string AlignAlgorithmAutomaticAlignStatusChecking =
                "#UI_AlignAlgorithmAutomaticAlignStatusChecking";

            public const string AlignAlgorithmAutomaticAlignStatusMainMoveJackCorrectPitchFront =
                "#UI_AlignAlgorithmAutomaticAlignStatusMainMoveJackCorrectPitchFront";

            public const string AlignAlgorithmAutomaticAlignStatusMainMoveJackCorrectPitchBack =
                "#UI_AlignAlgorithmAutomaticAlignStatusMainMoveJackCorrectPitchBack";

            public const string AlignAlgorithmAutomaticAlignStatusMainMoveJackCorrectRollLeft =
                "#UI_AlignAlgorithmAutomaticAlignStatusMainMoveJackCorrectRollLeft";

            public const string AlignAlgorithmAutomaticAlignStatusMainMoveJackCorrectRollRight =
                "#UI_AlignAlgorithmAutomaticAlignStatusMainMoveJackCorrectRollRight";

            public const string AlignAlgorithmAutomaticAlignStatusFinalization =
                "#UI_AlignAlgorithmAutomaticAlignStatusFinalization";

            public const string MoveAlgorithm = "#UI_MoveAlgorithm";

            public const string MoveAlgorithmAutomaticMoveStatusChecking =
                "#UI_MoveAlgorithmAutomaticMoveStatusChecking";

            public const string MoveAlgorithmAutomaticMoveStatusRotateForwardRightBySide =
                "#UI_MoveAlgorithmAutomaticMoveStatusRotateForwardRightBySide";

            public const string MoveAlgorithmAutomaticMoveStatusRotateForwardLeftBySide =
                "#UI_MoveAlgorithmAutomaticMoveStatusRotateForwardLeftBySide";

            public const string MoveAlgorithmAutomaticMoveStatusRotateBackwardRightBySide =
                "#UI_MoveAlgorithmAutomaticMoveStatusRotateBackwardRightBySide";

            public const string MoveAlgorithmAutomaticMoveStatusRotateBackwardLeftBySide =
                "#UI_MoveAlgorithmAutomaticMoveStatusRotateBackwardLeftBySide";

            public const string MoveAlgorithmAutomaticMoveStatusMoveForward =
                "#UI_MoveAlgorithmAutomaticMoveStatusMoveForward";

            public const string MoveAlgorithmAutomaticMoveStatusMoveBackward =
                "#UI_MoveAlgorithmAutomaticMoveStatusMoveBackward";

            public const string MoveAlgorithmAutomaticMoveStatusFinalization =
                "#UI_MoveAlgorithmAutomaticMoveStatusFinalization";

            public const string UnjackAlgorithm = "#UI_UnjackAlgorithm";

            public const string UnjackAlgorithmAutomaticUnjackStatusWaiting =
                "#UI_UnjackAlgorithmAutomaticUnjackStatusWaiting";

            public const string UnjackAlgorithmAutomaticUnjackStatusFinalization =
                "#UI_UnjackAlgorithmAutomaticUnjackStatusFinalization";

            public const string Proceed = "#UI_Proceed";
            public const string Cancel = "#UI_Cancel";
            public const string UnitStatusChangeNotPermitted = "#UI_UnitStatusChangeNotPermitted";
            public const string DrillingLaunchType = "#UI_DrillingLaunchType";
            public const string DrillingLaunchTypeDrillingMustStop = "#UI_DrillingLaunchTypeDrillingMustStop";
            public const string DrillingLaunchTypeDrillingMustStart = "#UI_DrillingLaunchTypeDrillingMustStart";
            public const string EmergencyRepairs = "#UI_STATUS_EREP";
            public const string OtherOperations = "#UI_STATUS_OOP";
            public const string LackOfFuel = "#UI_STATUS_LOF";
            public const string ChangeStatusAsk = "#UI_ChangeStatusAsk";

            public const string ResponseStatusCodeUnsuccessfulNotification =
                "#UI_ResponseStatusCodeUnsuccessfulNotification";

            public const string CriticalLowBatteryChargeNotificationTitle =
                "#UI_CriticalLowBatteryChargeNotificationTitle";

            public const string CriticalLowBatteryChargeNotificationBody =
                "#UI_CriticalLowBatteryChargeNotificationBody";
        }

        public static class Descriptions
        {
            public const string DrillJointExtensionFakeDrillRod = "#UI_DrillJointExtensionFakeDrillRod";
        }

        public static class SetupUiDescriptions
        {
            public const string NextButtonContent = "#UI_NextButtonText";
            public const string PreviousButtonContent = "#UI_CancelButtonText";
            public const string HomePageTitle = "#UI_HomePageTitle";
            public const string HomePageDescription = "#UI_HomePageDescription";
            public const string HomePageCheckBoxUninstallContent = "#UI_HomePageCheckBoxUninstallContent";
            public const string ArchiveDialogPageTitle = "#UI_ArchiveDialogPageTitle";
            public const string ArchiveDialogPageDescription = "#UI_ArchiveDialogPageDescription";
            public const string OpenDialogButton = "#UI_OpenDialogButton";
            public const string SettingsPageTitle = "#UI_SettingsPageTitle";
            public const string SettingsPageDescription = "#UI_SettingsPageDescription";
            public const string StartExtractButtonContent = "#UI_StartExtractButtonContent";
            public const string TextBoxArchiveBuildInputLabel = "#UI_TextBoxArchiveBuildInputLabel";
            public const string TextBoxDirectoryOutputLabel = "#UI_TextBoxDirectoryOutputLabel";

            public const string SettingsPageSelectedDirectorySourceLabel =
                "#UI_SettingsPageSelectedDirectorySourceLabel";

            public const string FinishPageTitle = "#UI_FinishPageTitle";
            public const string FinishPageDescription = "#UI_FinishPageDescription";
            public const string FinishButtonContent = "#UI_FinishButtonContent";
            public const string StartDemonContent = "#UI_StartDemonContent";
            public const string InstallDependenciesPageTitle = "#UI_InstallDependenciesPageTitle";
            public const string InstallDependenciesPageDescription = "#UI_InstallDependenciesPageDescription";
            public const string SaveButtonContent = "#UI_SaveButtonContent";
            public const string UninstallPageTitle = "#UI_UninstallPageTitle";
            public const string UninstallPageDescription = "#UI_UninstallPageDescription";
        }
    }

    public static class Log
    {
        //TODO: Remove and use string.Empty;
        public const string Empty = "";
        public const string BackgroundTaskFailed = "Background task of {0} failed!";
        public const string BackgroundTaskStarting = "Starting background task of {0}!";
        public const string BackgroundTaskStopping = "Stopping background task of {0}!";
        public const string BackgroundTaskStopped = "Stopped background task of {0}!";
    }
}