using CustomApiRessource.Enums;

namespace CustomApiResponse.Models;

public class ApiResponse
{
    public int Code { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }
    public string Description { get; set; }

    public ApiResponse() { }

    public ApiResponse(int code, string message = null, string description = null)
    {
        Code = code;
        Message = message ?? GetDefaultMessageForStatusCode(code);
        Description = description;
    }

    private static string GetDefaultMessageForStatusCode(int code)
    {
        return code switch
        {
            (int) CustomHttpCode.ERROR => "Il y a une erreur, veuillez réessayer ou contacter l'administrateur !",
            (int) CustomHttpCode.SUCCESS => "Opération déroulée avec succès",
            (int) CustomHttpCode.WARNING =>
                "Des avertissements ont été identifiés, veuillez les consulter avec attention !",
            (int) CustomHttpCode.INVALID_USER => "Nom d'utilisateur ou mot de passe invalide",
            (int) CustomHttpCode.OBJECT_NOT_FOUND =>
                "Aucune donnée trouvée en utilisant les paramètres spécifiques. Veuillez réessayer ou contacter votre équipe d'assistance",
            (int) CustomHttpCode.OBJECT_ALREADY_EXISTS => "Conflits d'objets identifiés",
            (int) CustomHttpCode.PROCESS_ERRORS =>
                "Des erreurs ont été détectées lors du process de calcul des données du dictionnaire. Veuillez réessayer ou contacter votre équipe d'assistance",
            (int) HttpCode.MOVED_PERMANENTLY => "Document déplacé de façon permanente",
            (int) HttpCode.FOUND => "Document déplacé de façon temporaire",
            (int) HttpCode.BAD_REQUEST => "La syntaxe de la requête est incorrecte",
            (int) HttpCode.UNAUTHORIZED => "Une authentification est nécessaire pour accéder à la ressource",
            (int) HttpCode.PAYMENT_REQUIRED => "Paiement requis pour accéder à la ressource",
            (int) HttpCode.FORBIDDEN => "Accès refusé",
            (int) HttpCode.NOT_FOUND => "Page ou ressource non trouvée",
            (int) HttpCode.METHOD_NOT_ALLOWED => "Méthode non autorisée",
            (int) HttpCode.INTERNAL_SERVER_ERROR => "Erreur interne du serveur",
            (int) HttpCode.SERVICE_UNAVAILABLE => "Service temporairement indisponible ou en maintenance",
            _ => null
        };
    }
}
