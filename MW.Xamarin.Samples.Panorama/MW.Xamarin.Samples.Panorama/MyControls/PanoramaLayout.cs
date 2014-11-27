using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MW.Samples.Panorama.MyControls
{
    public class PanoramaLayout : Layout<PanoramaCellImage>
    {
        public double RowHeight
        {
            get;
            set;
        }

        bool _firstChild;

        public void OnNext()
        {
            PanoramaCellImage cellCurrent = Children.FirstOrDefault(x => x.IsVisiblePanorama);
            if (cellCurrent == null)
                return;
            PanoramaCellImage cellNext = GetChildren(cellCurrent, 1);
            if (cellNext != null)
            {
                cellCurrent.IsVisiblePanorama = false;
                cellNext.IsVisiblePanorama = true;
                this.ForceLayout();
            }
        }

        public void OnPrevious()
        {
            PanoramaCellImage cellCurrent = Children.FirstOrDefault(x => x.IsVisiblePanorama);
            if (cellCurrent == null)
                return;
            PanoramaCellImage cellNext = GetChildren(cellCurrent, -1);
            if (cellNext != null)
            {
                cellCurrent.IsVisiblePanorama = false;
                cellNext.IsVisiblePanorama = true;
                this.ForceLayout();
            }
        }

        private PanoramaCellImage GetChildren(Image cellCurrent, int pas)
        {
            PanoramaCellImage cellSearch = null;
            int i = 0;
            while (cellSearch != cellCurrent && i <= Children.Count)
            {
                cellSearch = Children[i] as PanoramaCellImage;
                i++;
            }
            if (i + pas - 1 < Children.Count && i + pas - 1 >= 0)
                return Children[i + pas - 1] as PanoramaCellImage;
            else
                return null;
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            PanoramaCellImage childCell = child as PanoramaCellImage;
            if (childCell != null)
            {
                childCell.PanoramaView = this;
                if (!_firstChild)
                {
                    childCell.IsVisiblePanorama = true;
                    _firstChild = true;
                }
            }
            else
                throw new Exception("Non authorisé !");
        }

        public void OnSizeChanged()
        {
            this.ForceLayout();
        }

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            if (WidthRequest > 0)
                widthConstraint = Math.Min(widthConstraint, WidthRequest);
            if (RowHeight > 0)
                heightConstraint = Math.Min(heightConstraint, RowHeight);

            double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);
            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);

            return new SizeRequest(new Size(internalWidth, internalHeight), new Size(internalWidth, internalHeight));
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            // position du panorama dans l'écran
            double yPos = y;
            double xPos = x;

            List<PanoramaCellImage> cellsVisible = Children.Where(c => c.IsVisible).Cast<PanoramaCellImage>().ToList();

            PanoramaCellImage cell = cellsVisible.FirstOrDefault(c => c.IsVisiblePanorama);

            if (cell != null)
            {
                // Demande de la place disponible pour l'affichage du composant
                var request = cell.GetSizeRequest(width, height);
                // Calcul de la largueur d'une tuile
                double childWidth = width - cell.Spacing * 2 - cell.PreviousCell * 2;
                // Calcul de la hauteur de la ligne disponible
                double childHeight = Math.Max(RowHeight, request.Minimum.Height);
                if (VerticalOptions.Expands)
                    childHeight = height;

                if (VerticalOptions.Alignment == LayoutAlignment.End)
                    yPos = childHeight;
                else if (VerticalOptions.Alignment == LayoutAlignment.Center)
                    yPos = (height - childHeight) / 2;
                else if (VerticalOptions.Alignment == LayoutAlignment.Start)
                    yPos = 0;
                else if (VerticalOptions.Alignment == LayoutAlignment.Fill)
                    yPos = 0;

                // Si pas encore de layout pour les élements, alors on est sur le premier affichage du panorama
                if (cell.IsLayoutLoad == null)
                {
                    // Dans ce cas, faire le calcul de tout les rectangles
                    foreach (var item in cellsVisible)
                    {
                        xPos = xPos + cell.Spacing + cell.PreviousCell;
                        var region = new Rectangle(xPos + cell.Spacing + cell.PreviousCell, yPos, childWidth, childHeight);
                        LayoutChildIntoBoundingRegion(cell, region);
                    }
                }
                else
                {
                    double xPosCurrent = xPos + cell.Spacing + cell.PreviousCell;
                    double xPosPrevious = xPos - childWidth + cell.PreviousCell;
                    double xPosNext = xPos + cell.Spacing * 2 + cell.PreviousCell + childWidth;
                    double xPosInvisible = xPos + childWidth * 2;

                    // On déplace le current cell
                    var region = new Rectangle(xPosCurrent, yPos, childWidth, childHeight);
                    cell.LayoutTo(region);

                    // Recherche de la cell suivante
                    PanoramaCellImage cellnext = GetChildren(cell, 1);
                    if (cellnext != null)
                    {
                        // déplacement de la cell suivante
                        var regionNext = new Rectangle(xPosNext, yPos, childWidth, childHeight);
                        cellnext.LayoutTo(regionNext);
                    }

                    // recherche de la cell précendente
                    PanoramaCellImage cellprevious = GetChildren(cell, -1);
                    if (cellprevious != null)
                    {
                        // déplacement de cell précendente
                        var regionPrevious = new Rectangle(xPosPrevious, yPos, childWidth, childHeight);
                        cellprevious.LayoutTo(regionPrevious);
                    }

                    // déplacement de toutes les autres cells dans une zone invisible
                    foreach (PanoramaCellImage item in cellsVisible.Where(c => c != cell && c != cellprevious && c != cellnext).ToList())
                    {
                        var regionItem = new Rectangle(xPosInvisible, yPos, childWidth, childHeight);
                        item.Layout(regionItem);
                    }
                }
            }
        }
    }
}
