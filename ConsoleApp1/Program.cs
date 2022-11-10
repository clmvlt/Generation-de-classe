namespace ConsoleApp1
{
    internal class Program
    {
        static List<string> lesChoix = new List<string>();
        static List<Attribu> lesAttribus = new List<Attribu>();

        static string? nomClasse, nomNamespace;

        public static int Selecteur()
        {
            Console.Clear();
            for (int i = 0; i < lesChoix.Count; i++)
                Console.WriteLine($"{i + 1}. {lesChoix[i]}");
            string? rl = Console.ReadLine();
            int choix = rl == null ? 0 : int.Parse(rl);
            if (choix < 1 || choix > lesChoix.Count) ConsoleErreur("Mauvais choix.");
            return choix;
        }

        public static void ConsoleErreur(string message)
        {
            Console.Clear();
            Console.WriteLine("Une erreur s'est produite.\n" + message);
            Thread.Sleep(2000);
            Selecteur();
        }

        public static Attribu CreerAttribu()
        {
            string? name, type;
            bool isGetter, isSetter;

            do
            {
                Console.WriteLine("Entrez le nom de l'attribu :");
                name = Console.ReadLine();
                if (name == null || name.Equals(String.Empty)) Console.WriteLine("Le nom de l'attribu ne peut pas être vide.");
            } while (name == null || name.Equals(String.Empty));

            do
            {
                Console.WriteLine("Entrez le type de l'attribu :");
                type = Console.ReadLine();
                if (type == null || type.Equals(String.Empty)) Console.WriteLine("Le type de l'attribu ne peut pas être vide.");
            } while (type == null || type.Equals(String.Empty));

            string? temp;
            int choix;
            do
            {
                Console.WriteLine("L'attribu possèdera-t-il un Getteur ?\n0 = NON\n1 = OUI");
                temp = Console.ReadLine();
                choix = temp == null ? 0 : int.Parse(temp);
                if (choix != 0 && choix != 1) Console.WriteLine("Choix valide.");
            } while (choix != 0 && choix != 1);
            isGetter = choix == 1;

            do
            {
                Console.WriteLine("L'attribu possèdera-t-il un Setteur ?\n0 = NON\n1 = OUI");
                temp = Console.ReadLine();
                choix = temp == null ? 0 : int.Parse(temp);
                if (choix != 0 && choix != 1) Console.WriteLine("Choix valide.");
            } while (choix != 0 && choix != 1);
            isSetter = choix == 1;

            do
            {
                Console.WriteLine($"Voice les détails de l'attribu :\nNom : {name}\nType : {type}\nGetteur : {isGetter}\nSetteur : {isSetter}\nÊtes-vous sûr de vouloir ajouter l'attribu ?\n0 = NON\n1 = OUI");
                temp = Console.ReadLine();
                choix = temp == null ? 0 : int.Parse(temp);
                if (choix != 0 && choix != 1) Console.WriteLine("Choix valide.");
            } while (choix != 0 && choix != 1);

            if (choix == 0) return null;
            return new Attribu(name, type, isGetter, isSetter);
        }

        public static void AfficherAttribu(Attribu attribu)
        {
            Console.WriteLine(attribu.ToString());
        }

        public static void AfficherEntrerQuitter()
        {
            Console.WriteLine("Appuyez sur entrer pour quitter.");
            Console.ReadKey(true);
        }

        private static void AfficherLesAttribus()
        {
            Console.Clear();
            foreach (Attribu at in lesAttribus) AfficherAttribu(at);
            AfficherEntrerQuitter();
        }

        public static void Main()
        {
            lesChoix.Add("Ajouter un attribus.");
            lesChoix.Add("Lister les attribus.");
            lesChoix.Add("Afficher la classe.");
            lesChoix.Add("Quitter.");

            do
            {
                Console.WriteLine("Entrez le nom du namespace :");
                nomNamespace = Console.ReadLine();
                if (nomNamespace == null || nomNamespace.Equals(String.Empty)) Console.WriteLine("Le nom du namespace ne peut pas être vide.");
            } while (nomNamespace == null || nomNamespace.Equals(String.Empty));
            Console.WriteLine("Le nom du namespace est " + nomNamespace);

            do
            {
                Console.WriteLine("Entrez le nom de la classe");
                nomClasse = Console.ReadLine();
                if (nomClasse == null || nomClasse.Equals(String.Empty)) Console.WriteLine("Le nom de la classe ne peut pas être vide.");
            } while (nomClasse == null || nomClasse.Equals(String.Empty));

            int choix;
            do
            {
                choix = Selecteur();
                switch (choix)
                {
                    case 1:
                        Attribu attribu = CreerAttribu();
                        if (attribu == null)
                        {
                            Console.WriteLine("Ajout annulé!");
                        } else
                        {
                            Console.WriteLine("Attribu ajouté!");
                            lesAttribus.Add(attribu);
                        }
                        AfficherEntrerQuitter();
                        break;
                    case 2:
                        AfficherLesAttribus();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine(GenererClass());
                        AfficherEntrerQuitter();
                        break;
                }
            } while (choix != lesChoix.Count);
        }

        public static string GenererClass()
        {
            string classe = $"namespace {nomNamespace}\n" +
                "{\n" +
                "class " + nomClasse +
                "\n{\n" +
                "#region Attribu";


            foreach (Attribu attribu in lesAttribus)
            {
                classe += $"\nprivate _{attribu.Type} {attribu.Name};";
            }
            classe += "\n#endregion\n\n#region Constructeur\n" +
                "public " + nomClasse + "(";

            foreach (Attribu attribu in lesAttribus)
            {
                classe += $"{attribu.Type} {attribu.Name}";
                if (lesAttribus.Last() != attribu) classe += ", ";
            }

            classe += ")\n{";

            foreach (Attribu attribu in lesAttribus)
            {
                classe += $"\n_{attribu.Name} = {attribu.Name};";
            }

            classe += "\n}\n#endregion\n\n#region Properties\n";

            foreach (Attribu attribu in lesAttribus)
            {
                if (attribu.IsGetter || attribu.IsSetter)
                {
                    string nameCamel = attribu.Name[0].ToString().ToUpper() + attribu.Name.Substring(1);
                    classe += $"\npublic {attribu.Type} {nameCamel} " + "{ ";

                    if (attribu.IsGetter) classe += $"get => _{attribu.Name}; ";
                    if (attribu.IsSetter) classe += $"set => _{attribu.Name} = value; ";

                    classe += " }";
                }
            }

            classe += "\n#endregion\n}\n}";
            return classe;
        }
    }
}