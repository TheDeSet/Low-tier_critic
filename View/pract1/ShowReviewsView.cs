using Lab_3._1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class ShowReviewsView : UserControl
    {
        public ShowReviewsView()
        {
            InitializeComponent();
        }
        public void SetReview(Review review)
        {
            if (review == null) return;
            PIC_UserIcon.Image = View.Properties.Resources.User;
            LB_Username.Text = $"{review.Username ?? "Аноним"}";
            LB_Rating.Text = $"{review.Rating:F1} из 5.0";
            RTB_ReviewText.Text = review.ReviewText ?? "Рецензия отсутствует.";
        }
    }
}
