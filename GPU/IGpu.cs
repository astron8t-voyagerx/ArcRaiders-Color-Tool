namespace ArcRaiders_Color_Tool.GPU
{
    internal interface IGPU
    {
        GPUVendor Vendor { get; }
        int Saturation { get; set; }
        int MaxSaturation { get; }
        int MinSaturation { get; }
        int InitSaturation { get; }
        void ResetSaturation();
        void Load(string display);
        void Close();
    }
}
