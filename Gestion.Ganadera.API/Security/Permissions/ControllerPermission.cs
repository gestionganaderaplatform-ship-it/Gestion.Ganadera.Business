namespace Gestion.Ganadera.API.Security.Permissions
{
    /// <summary>
    /// Define permisos combinables para habilitar o ocultar acciones del controller base.
    /// </summary>
    [Flags]
    public enum ControllerPermission
    {
        None = 0,
        GetById = 1 << 0,
        GetAll = 1 << 1,
        Create = 1 << 2,
        CreateMany = 1 << 3,
        Update = 1 << 4,
        Delete = 1 << 5,
        DeleteByForeignKey = 1 << 6,
        GetByForeignKey = 1 << 7,
        Exists = 1 << 8,
        ExistsMany = 1 << 9,
        GetPaged = 1 << 10,
        ExistsByForeignKey = 1 << 11,
        Filter = 1 << 12,
        ExportExcel = 1 << 13,
        ImportExcel = 1 << 14,
        DownloadExcelTemplate = 1 << 15,
        All = GetById | GetAll | Create | CreateMany |
              Update | Delete | DeleteByForeignKey |
              GetByForeignKey | Exists | ExistsMany | GetPaged |
              ExistsByForeignKey | Filter | ExportExcel |
              ImportExcel | DownloadExcelTemplate,
        Standard = GetById | GetAll | Create |
                   Update | Delete | Exists | ExistsMany
    }
}
