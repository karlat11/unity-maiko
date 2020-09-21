using UnityEngine;

public class textCreator : MonoBehaviour
{
    public string[] sentences;

    private string[] characterCreatorFacts;

    private void Start()
    {
        characterCreatorFacts = new string[]
        {
            "Did you know that Geisha and Maiko have to follow different appearance rules to signify their differences?  The 3 main categories that vary are: hairstyles, make-up and kimono design. Maiko use their own hair and 'hana-kanzashi' or a silk flower comb with ornaments to decorate their hair. Meanwhile, Geisha use wigs and simple decorative combs or 'kanzashi'.",
            "Maiko cover their face in white while leaving a strip of bare skin by the hairline, use pink blush around their cheeks, crimson and black outlines around their eyes, define eyebrows with red or pink under the black and cover their lips only partially in red. Geisha cover their face in white entirely, outline their eyes with a black line, use a faint red under the black of their eyebrows and can cover their lips fully with red.",
            "Maiko wear colourful kimonos with dominating red colour and a wide 'obi' tied into a bow at the back, the collar is thick and embroidered. Geisha wear usually a single colour kimonos with narrower 'obi' tied in a simple box bow at the back and white collar."
        };

        sentences = characterCreatorFacts;
    }
}
