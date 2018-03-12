Klient BooksWPFClient.exe se nachází na Books-db-with-client-and-server\BooksWPFClient\bin\Debug\BooksWPFClient.exe
Při spuštění ve Visual Studiu je třeba nastavit jako startup projekt server, spustit, potom nastavit jako startup projekt klienta, případně změnit port na řádku 85 v souboru MainWindow.xaml.cs
Server je založen na technologii .Net Core, načte data o knihách z databáze, umožňuje přidávat nové knihy a aktualizovat stávající. 
Klient je wpf aplikace, UI zobrazí údaje o knihách. Komunikace probíhá přes REST. 
