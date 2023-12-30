using Utilities.Enums;

namespace Utilities.Handlers
{
    public static class GetNameHandler
    {
        public static string GetVendorStatusName(VendorStatusEnum vendorStatus)
        {
            if (vendorStatus == VendorStatusEnum.Pending) return nameof(VendorStatusEnum.Pending);
            if (vendorStatus == VendorStatusEnum.Accepted) return nameof(VendorStatusEnum.Accepted);
            if (vendorStatus == VendorStatusEnum.ApproveByAdmin) return nameof(VendorStatusEnum.ApproveByAdmin);
            else return nameof(VendorStatusEnum.Rejected);
        }
    }
}
