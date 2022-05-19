namespace School.Infrastructure.Persistence.Mapping;

public class ModuleDatabase
{
    private ModuleDatabase(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ModuleDatabase ModuloAlmoxarifado { get { return new ModuleDatabase("alm_"); } }
    public static ModuleDatabase ModuloCadastrosGerais { get { return new ModuleDatabase("cad_"); } }
    public static ModuleDatabase ModuloCaptacaoRecepcao { get { return new ModuleDatabase("cap_"); } }
    public static ModuleDatabase ModuloCobranca { get { return new ModuleDatabase("cob_"); } }
    public static ModuleDatabase ModuloCompras { get { return new ModuleDatabase("com_"); } }
    public static ModuleDatabase ModuloContabil { get { return new ModuleDatabase("con_"); } }
    public static ModuleDatabase ModuloFinanceiro { get { return new ModuleDatabase("fin_"); } }
    public static ModuleDatabase ModuloFiscal { get { return new ModuleDatabase("fis_"); } }
    public static ModuleDatabase ModuloIntegracao { get { return new ModuleDatabase("int_"); } }
    public static ModuleDatabase ModuloMensageria { get { return new ModuleDatabase("men_"); } }
    public static ModuleDatabase ModuloPosVendas { get { return new ModuleDatabase("pos_"); } }
    public static ModuleDatabase ModuloSeguranca { get { return new ModuleDatabase("seg_"); } }
    public static ModuleDatabase ModuloTelemarketing { get { return new ModuleDatabase("tel_"); } }
    public static ModuleDatabase ModuloVendas { get { return new ModuleDatabase("ven_"); } }
}