namespace NotifiNoteBE.Configurations
{
	public class JwtConfig
	{

		public string ValidAudience {  get; set; }
		public string ValidIssuer {  get; set; }
		public string Secret {  get; set; }
	}
}
