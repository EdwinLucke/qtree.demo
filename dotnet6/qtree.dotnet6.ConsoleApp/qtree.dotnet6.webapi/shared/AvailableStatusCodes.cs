using System.Net;

namespace qtree.dotnet6.webapi.dockerized.shared
{
    /// <summary>
    /// https://movares-ta.visualstudio.com/LTD/_wiki/wikis/LTD.wiki/1567/HTTP-response-codes-guidelines
    /// 200 Ok
    ///        Geen toast
    ///Gebruiken bij alle succesvolle gets
    ///201 Create
    ///Groene toast
    ///Gebruiken bij succesvolle posts
    ///400 Bad Request
    ///Oranjetoast
    ///Gebruiken bij wel een succesvolle call maar waarbij validatie errors zijn
    ///403 Forbidden
    ///rode toast
    ///Gebruiken bij als een gebruiker met een bepaalde rol een actie niet mag vervullen.
    ///409 Conflict
    ///Oranjetoast en / groene toast
    ///Gebruiken bij bulk calls, succesmeldingen en conflict meldingen(eentje uit de lijst waar de change/update/delete niet van kan) bundelen
    ///415 Unsupported media type
    ///Rode roast
    ///Gebruiken bij het uploaden van een bestand met de verkeerde extentie
    ///500 Bad Request
    ///Rode toast
    /// </summary>
    public enum AvailableStatusCodes
    {
        Ok = HttpStatusCode.OK,
        Create = HttpStatusCode.Created,
        NotModified = HttpStatusCode.NotModified,
        BadRequest = HttpStatusCode.BadRequest,
        ForBidden = HttpStatusCode.Forbidden,
        Conflict = HttpStatusCode.Conflict,
        UnsupportedMediaType = HttpStatusCode.UnsupportedMediaType,
        InternalServerError = HttpStatusCode.InternalServerError
    }
}
