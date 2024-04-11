using System.Security.Cryptography;

string choixContinuer = "";

do
{
    AffichageInitial();

    ChoisirSensCrypto();

    choixContinuer = AffichageFinal(choixContinuer);
}
while (choixContinuer != "n");

Console.WriteLine("\nÀ la prochaine!");





void ChoisirSensCrypto()
{
    string choixCryptogramme = "";

    while (choixCryptogramme != "1" && choixCryptogramme != "2")
    {
        Console.WriteLine("Tapez 1 pour encoder un message ou 2 pour décoder un message :");
        choixCryptogramme = Console.ReadLine();

        if (choixCryptogramme == "1")
        {
            string intrant = ValiderMessage();
            string cleSubstitution = ChoisirCle();
            string cleTransposition = ChoisirCle();
            string extrant = Encoder(intrant, cleSubstitution, cleTransposition);

            if (extrant != "")
            {
                Console.WriteLine("\nVoici votre message encodé :\n" + extrant);
            }

            else
            {
                Console.WriteLine("Erreur.");
            }

        }

        else if (choixCryptogramme == "2")
        {
            string intrant = ValiderMessage();
            string cleSubstitution = ChoisirCle();
            string cleTransposition = ChoisirCle();
            string extrant = Decoder(intrant, cleSubstitution, cleTransposition);

            if (extrant != "")
            {
                Console.WriteLine("\nVoici votre message décodé :\n" + extrant);
            }

            else
            {
                Console.WriteLine("Erreur.");
            }
        }
    }
}

string ValiderMessage()
{
    string intrant = "";
    string alphabet = RetourneAlphabet();

    while (intrant == "")
    {
        Console.WriteLine("\nVeuillez entrer le message à encoder ou à décoder. " +
                        "Ce message doit faire au maximum 64 caractères de long :");
        intrant = Console.ReadLine();

        if (intrant.Length <= 64)
        {
            for (int i = intrant.Length; i < alphabet.Length; i++)
            {
                intrant = intrant + alphabet[Randomiser(0, intrant.Length - 1)].ToString();
            }
        }

        else
        {
            Console.WriteLine("Votre message ne fait pas 64 caractères de long.");
            intrant = "";
        }
    }

    return intrant;
}

string ChoisirCle()
{
    string cle = "";
    string choixCle = "0";

    while (choixCle != "1" && choixCle != "2")
    {
        Console.WriteLine("\nTapez 1 pour créer une clé ou 2 pour entrer une clé :");
        choixCle = Console.ReadLine();

        if (choixCle == "1")
        {
            cle = CreerCle();
        }

        else if (choixCle == "2")
        {
            cle = EntrerCle();
        }
    }

    return cle;
}

string EntrerCle()
{
    string alphabet = RetourneAlphabet();
    string cle = "";

    while (cle == "")
    {
        Console.WriteLine("\nVeuillez entrer votre clé. Cette clé doit faire 64 caractères de long :");
        cle = Console.ReadLine();

        if (cle.Length == 64)
        {
            foreach (char c in cle)
            {
                if (!alphabet.Contains(c.ToString()))
                {
                    Console.WriteLine("Votre clé contient des caractères illégaux.");
                    cle = "";
                    break;
                }
            }
        }

        else
        {
            Console.WriteLine("Votre clé ne fait pas 64 caractères de long.");
            cle = "";
        }
    }

    return cle;
}

string CreerCle()
{
    string alphabet = RetourneAlphabet();
    string cle = "";

    for (int i = 0; i < alphabet.Length; i++) { cle += alphabet[Randomiser(0, alphabet.Length - 1)]; }

    Console.WriteLine("\nVoici votre clé :");

    foreach (char symbole in cle) { Console.Write(symbole); }

    Console.Write("\n");

    return cle;
}

string Encoder(string intrant, string cleSubstitution, string cleTransposition)
{
    string alphabet = RetourneAlphabet();
    string[] texte = new string[intrant.Length];

    for (int i = 0; i < intrant.Length; i++)
    {
        texte[i] = intrant[i].ToString();
    }

    for (int i = 0; i < intrant.Length; i++)
    {
        int position1 = i;
        int position2 = alphabet.IndexOf(cleTransposition[i].ToString());

        int positionFinale = (position1 + position2) % alphabet.Length;

        string valeurTemp = texte[i];
        texte[i] = texte[positionFinale];
        texte[positionFinale] = valeurTemp;
    }

    string extrant = "";

    for (int i = 0; i < intrant.Length; i++)
    {
        int position1 = alphabet.IndexOf(texte[i].ToString());
        int position2 = alphabet.IndexOf(cleSubstitution[i].ToString());

        if (position1 != -1)
        {
            int positionFinale = (position1 + position2) % alphabet.Length;
            extrant += alphabet[positionFinale];
        }

        else
        {
            Console.WriteLine("Votre message contient des caractères illégaux.");
            return "";
        }
    }

    return extrant;
}

string Decoder(string intrant, string cleSubstitution, string cleTransposition)
{
    string alphabet = RetourneAlphabet();
    string[] texte = new string[intrant.Length];

    for (int i = 0; i < intrant.Length; i++)
    {
        int coordonnee1 = alphabet.IndexOf(intrant[i].ToString());
        int coordonnee2 = alphabet.IndexOf(cleSubstitution[i].ToString());

        if (coordonnee1 != -1)
        {
            int cooordonneFinale = (coordonnee1 - coordonnee2 + alphabet.Length) % alphabet.Length;
            texte[i] = alphabet[cooordonneFinale].ToString();
        }

        else
        {
            Console.WriteLine("Votre message contient des caractères illégaux.");
            return "";
        }
    }

    for (int i = intrant.Length - 1; i >= 0; i--)
    {
        int position1 = i;
        int position2 = alphabet.IndexOf(cleTransposition[i].ToString());

        int positionFinale = (position1 + position2) % alphabet.Length;

        string valeurTemp = texte[i];
        texte[i] = texte[positionFinale];
        texte[positionFinale] = valeurTemp;
    }

    string extrant = "";

    foreach (string symbole in texte)
    {
        extrant += symbole;
    }

    return extrant;
}

string AffichageFinal(string continuer)
{
    Console.WriteLine("\nVoulez-vous continuer? Tapez \"o\" pour continuer ou \"n\" pour quitter :");
    choixContinuer = Console.ReadLine().ToLower();

    if (choixContinuer != "")
    {
        choixContinuer = choixContinuer.Substring(0, 1);
    }

    Console.WriteLine("\n");

    return choixContinuer;
}

void AffichageInitial()
{
    Console.WriteLine("Bienvenue dans mon programme de chiffrement polyalphabétique par substitution et par transposition!\n\n" +
        "Voici une liste des symboles valides dans mon programme :");

    string alphabet = RetourneAlphabet();

    for (int i = 0; i < alphabet.Length; i++)
    {
        Console.Write(alphabet[i]);
    }

    Console.WriteLine("\n");
}

int Randomiser(int valeurMin, int valeurMax)
{
    return RandomNumberGenerator.GetInt32(valeurMin, valeurMax + 1);
}

string RetourneAlphabet()
{
    string[] SYMBOLES_CHIFFRES = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    string[] SYMBOLES_MINUSCULES = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    string[] SYMBOLES_MAJUSCULES = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    string[] SYMBOLES_AUTRES = { " ", "." };
    string alphabet = "";

    for (int i = 0; i < SYMBOLES_CHIFFRES.Length; i++) { alphabet += SYMBOLES_CHIFFRES[i]; }

    for (int i = 0; i < SYMBOLES_MINUSCULES.Length; i++) { alphabet += SYMBOLES_MINUSCULES[i]; }

    for (int i = 0; i < SYMBOLES_MAJUSCULES.Length; i++) { alphabet += SYMBOLES_MAJUSCULES[i]; }

    for (int i = 0; i < SYMBOLES_AUTRES.Length; i++) { alphabet += SYMBOLES_AUTRES[i]; }

    return alphabet;
}
