using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MindfireBlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        public UserHand playerHand;
        public UserHand dealerHand;
        public Deck currentDeck;
        static int playerScore = 0;
        static int dealerScore = 0;
        // Hit and Stand disabled until cards dealt
        private void btnDeal_Click(object sender, RoutedEventArgs e)
        {
            btnHit.IsEnabled = true;
            btnStand.IsEnabled = true;
            btnDeal.IsEnabled = false;

            // create the deck and player hands
            currentDeck = new Deck();
            playerHand = new UserHand(currentDeck.dealCard(), currentDeck.dealCard());
            lblPlayerCards.Content = playerHand.cards[0].cardWorth.ToString() + " ";
            lblPlayerCards.Content += playerHand.cards[1].cardWorth.ToString() + " ";

            dealerHand = new UserHand(currentDeck.dealCard(), currentDeck.dealCard());
            // Dealer's first card always blind
            lblDealerCards.Content = "?? ";

            // set player hand worth in label
            playerHand.handWorth = playerHand.cards[0].cardWorth + playerHand.cards[1].cardWorth;
            lblPlayerHandWorth.Content += playerHand.handWorth.ToString();

            // show one dealer card
            lblDealerCards.Content += dealerHand.cards[1].cardWorth.ToString() + " ";
            // Don't show full value of hand until game is over
            lblDealerHandWorth.Content = "??";

            if (playerHand.handWorth == 21)
            {
                if (dealerHand.handWorth == 21)
                {
                    // player AND dealer get a point
                    bothWin(1);
                    return;
                }
                else
                {
                    // player gets a point
                    playerWins(1);
                    return;
                }
            }           
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            int currentWorth = playerHand.handWorth;
            Card newCard = currentDeck.dealCard();
            playerHand.addCardToHand(newCard);            
            currentWorth += newCard.cardWorth;
            lblPlayerHandWorth.Content = currentWorth.ToString();
            lblPlayerCards.Content += newCard.cardWorth.ToString() + " ";
            if (Bust(playerHand))
            {
                // player loses
                dealerWins(1);
                return;
            }
        }
        
        private void btnStand_Click(object sender, RoutedEventArgs e)
        {
            DealerTurn();
        }
        // this should only happen if the player hits 21 or hits stand
        public void DealerTurn()
        {
            btnHit.IsEnabled = false;
            btnStand.IsEnabled = false;

            if (dealerHand.handWorth < playerHand.handWorth)
            {
                while (dealerHand.handWorth < 17 || (dealerHand.handWorth < playerHand.handWorth && playerHand.handWorth <= 21))
                {
                    Card newCard = currentDeck.dealCard();
                    dealerHand.addCardToHand(newCard);
                    lblDealerCards.Content += newCard.cardWorth.ToString() + " ";
                }
            }

            if (Bust(dealerHand))
            {
                if (Bust(playerHand))
                {
                    // dealer still wins
                    dealerWins(1);
                    return;
                }
                else
                {
                    // dealer loses
                    playerWins(1);
                    return;
                }
            }
            else if (playerHand.handWorth == dealerHand.handWorth)
            {
                // player loses and dealer gets a point
                dealerWins(1);
                return;
            }
            else if (dealerHand.handWorth == 21)
            {
                dealerWins(2);
                return;
            }
            else if (dealerHand.handWorth > playerHand.handWorth)
            {
                dealerWins(1);
                return;
            }
            // This shouldn't be hit, but leaving here in case logic changes in future
            else if (dealerHand.handWorth == 21 && playerHand.handWorth == 21)
            {
                bothWin(1);
                return;
            }
        }

        public bool Bust(UserHand hand)
        {
            return (hand.handWorth > 21);
        }

        private void playerWins(int pointsGained)
        {
            lblWhoWon.Content = "Player wins";
            playerScore += pointsGained;
            lblPlayerScore.Content = playerScore.ToString();
            revealDealerHand();
        }

        private void dealerWins(int pointsGained)
        {
            lblWhoWon.Content = "Dealer wins";
            dealerScore += pointsGained;
            lblDealerScore.Content = dealerScore.ToString();
            revealDealerHand();
        }

        private void bothWin(int pointsGained)
        {
            lblWhoWon.Content = "Tie";
            playerScore += pointsGained;
            lblPlayerScore.Content = playerScore.ToString();
            dealerScore += pointsGained;
            lblDealerScore.Content = dealerScore.ToString();
            revealDealerHand();
        }

        private void revealDealerHand()
        {
            // Empty label so we can reveal hidden card
            lblDealerCards.Content = "";
            foreach (Card card in dealerHand.cards)
            {
                lblDealerCards.Content += card.cardWorth.ToString() + " ";
            }
            lblDealerHandWorth.Content = dealerHand.handWorth.ToString();
        }


    }
}
