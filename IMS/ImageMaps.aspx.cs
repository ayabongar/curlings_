using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImageMaps : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    void VoteMap_Clicked(Object sender, ImageMapEventArgs e)
    {
        //string coordinates;
        //string hotSpotType;
        //int yescount = ((ViewState["yescount"] != null) ? (int)ViewState["yescount"] : 0);
        //int nocount = ((ViewState["nocount"] != null) ? (int)ViewState["nocount"] : 0);

        //// When a user clicks the "Yes" hot spot,
        //// display the hot spot's name and coordinates.
        //if (e.PostBackValue.Contains("Yes"))
        //{
        //    yescount += 1;
        //    coordinates = Vote.HotSpots[0].GetCoordinates();
        //    hotSpotType = Vote.HotSpots[0].ToString();
        //    Message1.Text = "You selected " + hotSpotType + " " + e.PostBackValue + ".<br />" +
        //                    "The coordinates are " + coordinates + ".<br />" +
        //                    "The current vote count is " + yescount.ToString() +
        //          " yes votes and " + nocount.ToString() + " no votes.";
        //}

        //// When a user clicks the "No" hot spot,
        //// display the hot spot's name and coordinates.
        //else if (e.PostBackValue.Contains("No"))
        //{
        //    nocount += 1;
        //    coordinates = Vote.HotSpots[1].GetCoordinates();
        //    hotSpotType = Vote.HotSpots[1].ToString();
        //    Message1.Text = "You selected " + hotSpotType + " " + e.PostBackValue + ".<br />" +
        //                    "The coordinates are " + coordinates + ".<br />" +
        //          "The current vote count is " + yescount.ToString() +
        //          " yes votes and " + nocount.ToString() + " no votes.";
        //}

        //else
        //{
        //    Message1.Text = "You did not click a valid hot spot region.";
        //}

        //ViewState["yescount"] = yescount;
        //ViewState["nocount"] = nocount;
    }           

}