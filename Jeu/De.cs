class De
{
    ///Attributs
    char[] faces;



    ///Constructeur
    public De(char[] faces) 
    {  
        this.faces = faces; 
    }

    public char[] Faces { get { return faces; } }




    ///Méthodes obligatoires

    public void Lance(Random r) 
    { 

    }

    public string toString()
    {
        return ""; //enlève cette ligne, c'est juste pour compiler
    }

}