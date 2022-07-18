using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace selenium
{
  class Program
  {
    static void Main(string[] args) {

      String test_url = "https://web.whatsapp.com/";
      IWebDriver driver;

      driver = new ChromeDriver(@"C:\");

      driver.Navigate().GoToUrl(test_url);
      driver.Manage().Window.Maximize();

      WebDriverWait waitOpen = new WebDriverWait(driver, TimeSpan.FromSeconds(70));
      IWebElement pesquisa = waitOpen.Until(ExpectedConditions.ElementExists(By.XPath("//div[contains(@class,'_13NKt copyable-text')]")));

      string mensagem = "No chats, contacts or messages found";

      Queue<string> filaTelefone = new Queue<string>();
      
      var lines = File.ReadAllLines("C:\\contatos.csv");
      foreach (var line in lines)
        filaTelefone.Enqueue(line);

      while(filaTelefone.Count > 0){
        pesquisa.SendKeys(filaTelefone.Dequeue());

        List<IWebElement> e = new List<IWebElement>();

        Thread.Sleep(3000);
        
        e.AddRange(driver.FindElements(By.XPath("//div[contains(@class,'f8jlpxt4 r5qsrrlp hp667wtd')]")));
        if (e.Count > 0){
          IWebElement semContato = driver.FindElement(By.XPath("//div[contains(@class,'f8jlpxt4 r5qsrrlp hp667wtd')]")); 
          if (semContato.Text == mensagem){
            pesquisa.Clear();
            e.Clear();
            continue;
          }
          else{
            e.Clear();
            continue;
          }            
        }

        Thread.Sleep(3000);

        e.AddRange(driver.FindElements(By.XPath("//div[contains(@class,'_3OvU8')]")));
        if (e.Count > 0){
          IWebElement contato = driver.FindElement(By.XPath("//div[contains(@class,'_3OvU8')]"));
          contato.Click();
          e.Clear();
        }
        else{
          e.Clear();
          continue;
        }

        Thread.Sleep(3000);

        e.AddRange(driver.FindElements(By.XPath("(//div[contains(@class,'_13NKt copyable-text')])[2]")));
        if (e.Count > 0){
          IWebElement digitar = driver.FindElement(By.XPath("(//div[contains(@class,'_13NKt copyable-text')])[2]"));  
          IWebElement enviar = driver.FindElement(By.XPath("//div[contains(@class,'_3HQNh _1Ae7k')]"));  
          
          digitar.SendKeys("Boa noite, tudo bem?");
          enviar.Click();

          Thread.Sleep(3000);
          
          digitar.SendKeys("Meu nome é Gabriel Neres sou promoter do Clube Chalezinho");
          enviar.Click();
          
          Thread.Sleep(3000);
          
          digitar.SendKeys("...");
          enviar.Click();

          Thread.Sleep(3000);

          digitar.SendKeys("...");
          enviar.Click();
          
          Thread.Sleep(3000);

          pesquisa.Clear();
          e.Clear();
        }
        else{
          e.Clear();
          continue;
        }
      }

      driver.Quit();
    }
  }
}
