## Fejlesztés Folyamata

- Az alap koncepció egy olyan jellegű titkosító algoritmus elkészítése volt, amely inkább a kulcs méretét növeli a futási idő javítása érdekében.


- A legelső elképzelésemben sima vektorokat akartam használni a karakterek titkosítására. Ezt az ötletet a vektorok összeadása adta, mivel ott sem látszik az eredményen, hogy milyen komponensek összege.


- Itt jött képbe, hogy egy kordináta rendszer beli pont jelképezhetne egy karaktert.


- Ennek a programozásban megvalósított verziójában a kordináta rendszer egy kétdimenziós mátrix.


- Viszont gyorsan rá kellet ébrednem, hogy a sima irányított vektorokat nem a legszerencsésebb kölcséghatékonyan megvalósítani a programozásban.


- Itt eszembe jutott, hogy a fizika példákban is szétbontjuk a vektorokat olyan komponensekre, amik iránya megegyezik a kordináta rendszerrel. Én is ezt tettem, egy vektor helyett egy sor és egy oszlop vektort használtam a karakterek titkosítására.


- A nehezebb visszafejthetőség érdekében pedig arra gondoltam, hogy mivel egy szónak csak úgy van értelme, ha a megfelelő betűk következnek egymás után, ezért a vektor mindig ez előző karakter pozíciójából indul.


- Szintén a nehezebb viszsafejthetőség érdekében az algoritmus egy végtelenített kordináta rendszerren dolgozik. Ezt úgy kell érteni, hogy ha mondjuk a mátrix bal oldalán kilép, akkor ugyan abban a magasságban jön be a jobb oldalán.


- A következő ötletem az volt, hogy egy karakter többször is szerepelhet egy kulcsban. A titkosítás folyamat során pedig a program véletlen szerűen választ egyet és annak a pozicióját használja a vektor elkészítéséhez.


- A gyorsabb futás érdekében az algoritmus csinál magának egy szótárat a kulcsról. A kulcsok a karakterek, amik előfordulnak a kulcsban. Az értékek pedig a pozicióinak listája. Ezt a műveletett csak egyszer kell végrehajtani, a kulcs beállítása után. A pozíció továbbra is véletlen szerűen választódik a listából.


- Mind eddig elég jól olvasható volt a titkosított szöveg a tagolása miatt. Ezért egyszerűen kivattem a helyközöket a titkosított szövegből. Sajnos így már nem lehetett vissza fejteni a szöveget. Ezért "normalizáltam" a vektorokat. A normalizálás mindig az aktuális kulcs mérete alapján történik.


- Pl. egy 125 magasságú kulcson a normalizálás 3 számjegyre történik. Tehát a 9 hosszú függőleges vektor 009 formában lesz benne a titkosított szövegben.


- Itt derült ki, hogy elég zavaró ha egy vektor negatív, mivel a "-" jelek úgymond segítenek a tagolásban. A megoldás borzasztóan egyszerű volt. Az algoritmusnak nem engedtem meg, hogy a kordináta rendszerben negatív irányban mozogjon. Ez azért működik, mert a végtelenítésnek köszönhetően minden karakter elérhető csak pozitív irányba való mozgással. 


- Egy későbbi probléma amibe beleütköztem, az a hatalmas futási idő volt a titkosításnal, amit a véletlen választás okozott. A megoldásom pedig egy saját lista osztály definiálása volt, ami tárolja az azonos karakterek pozícióit és egy indexet. Kérésre vissza adja az indexen lévő pozíciót és megnöveli egyel az indexet. Ha eléri a lista végét akkor kever rajta egyet és lenulláza az indexet.


- Ez a megoldásom nem csak, hogy jelentősen javított a futási időn, de még garantálja is, hogy a több azonos karakter ki is van használva a titkosítás során.