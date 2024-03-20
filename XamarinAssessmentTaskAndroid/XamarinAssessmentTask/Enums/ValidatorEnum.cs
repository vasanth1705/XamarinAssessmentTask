namespace XamarinAssessmentTask.Enums
{
    public enum SaveBeneficiaryValidator
    {
        Success,
        ReachedMaxBeneficiary,
        InvalidMobileNumber,
        MobileNumberExists,
        Error,
        UnKnownError
    }

    public enum TopUpProcessValidator
    {
        Success,
        Error,
        InsufficientBalance,
        MaxTopUpLimit,
        MaxMultiTopUpLimit
    }

    public enum LoginValidator
    {
        Success,
        InvalidUsernameOrPassword,
        EnterAllFields
    }
}