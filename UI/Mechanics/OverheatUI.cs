using SiriusMod.Mechanics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Animations;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.Social.Steam;
using Terraria.UI;

namespace SiriusMod.UI.Mechanics
{
    internal class OverheatUI : UIState
    {
        private UIElement area;
        public static UIImage frame;
        private BarLine progressBar;

        public override void OnInitialize()
        {
            // Инициализация элемента
            area = new UIElement();
            // Отступ от левого края в пикселях/процентах
            area.Left.Set(-18, 0.5f);
            // Оступ от верхнего края в пикселя/процентах
            area.Top.Set(0, 0.55f);
            // Размер области UI, роляет ток где надо на что-то кликать
            area.Width.Set(36, 0f);
            area.Height.Set(12, 0f);

            frame = new UIImage(ModContent.Request<Texture2D>("SiriusMod/Assets/ExtraTextures/Kitkat"));
            frame.Left.Set(0, 0f);
            frame.Top.Set(0, 0f);
            frame.ImageScale = 1.5f;
            //frame.Width.Set(36, 0f);
            //frame.Height.Set(12, 0f);

            progressBar = new BarLine();
            progressBar.Left.Set(0, 0f);
            progressBar.Top.Set(0, 0f);

            //К основной области добавляешь все элементы, которые должны "крепиться" к ней
            area.Append(frame);
            frame.Append(progressBar);
            // Добавляешь саму область на экран
            Append(area);
        }

        // Отрисовка UI, чтобы ее отменить возращаем return;
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is not Overheat overheatItem) // && !Main.LocalPlayer.controlUseItem
            {
                return;
            }
        
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            if (Main.LocalPlayer.HeldItem.ModItem is not Overheat overheatItem) // && !Main.LocalPlayer.controlUseItem
            {
                return;
            }
            // Rectangle progressrect = frame.GetInnerDimensions().ToRectangle();
            // progressrect.Width = (int)(36 * MathHelper.Clamp((overheatItem.OverheatLevel / 420f), 0f, 1f));
            // Main.NewText($"Width - {progressrect.Width}");
            // spriteBatch.Draw((ModContent.Request<Texture2D>("SiriusMod/Assets/ExtraTextures/Kitkat_Bar").Value), new Rectangle(750, 464, 36, 12), Color.White);
            // Main.NewText(new Rectangle(progressrect.Left + 1, progressrect.Y, 1, progressrect.Height));
            //
        }
    }


    public class BarLine : UIElement
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            if (Main.LocalPlayer.HeldItem.ModItem is not Overheat overheatItem) // && !Main.LocalPlayer.controlUseItem
            {
                return;
            }
            Rectangle progressrect = OverheatUI.frame.GetInnerDimensions().ToRectangle();
            progressrect.Width = (int)(36 * MathHelper.Clamp((overheatItem.OverheatLevel / 420f), 0f, 1f));
            Main.NewText($"Width - {progressrect.Width}");
            spriteBatch.Draw((ModContent.Request<Texture2D>("SiriusMod/Assets/ExtraTextures/KitKat_Bar").Value), new Vector2(progressrect.Left + 20, progressrect.Y + 20), new Rectangle?(new Rectangle(0, 0, progressrect.Width, 12)), Color.White, 0f,
                (new Rectangle(0, 0, progressrect.Width, 12)).Size() / 2f, 1.5f, SpriteEffects.None, 0f);
            Main.NewText(new Rectangle(progressrect.Left + 1, progressrect.Y, 1, progressrect.Height));
            
        }
    }
    
    // gpt
    // public class UIImageBar : UIElement
    // {
    //     private Texture2D _texture;
    //     public float FillPercent = 1f; // от 0 до 1
    //     public float ImageScale { get; set; } = 1f;
    //
    //     public UIImageBar(Texture2D texture)
    //     {
    //         _texture = texture;
    //         // Задаём элементу размеры, чтобы не были нулевыми
    //         Width.Set(texture.Width, 0f);
    //         Height.Set(texture.Height, 0f);
    //     }
    //
    //     protected override void DrawSelf(SpriteBatch spriteBatch)
    //     {
    //         base.DrawSelf(spriteBatch);
    //
    //         // Берём фактические размеры, которые занял UIElement на экране
    //         var dimensions = GetDimensions().ToRectangle();
    //
    //         // Ширина "заполненной" части текстуры
    //         int fillWidth = (int)(_texture.Width * FillPercent);
    //         // Защита от дурака, если fillWidth < 0
    //         if (fillWidth < 0)
    //             fillWidth = 0;
    //         if (fillWidth > _texture.Width)
    //             fillWidth = _texture.Width;
    //
    //         // Исходная часть текстуры, которую рисуем
    //         Rectangle sourceRect = new Rectangle(0, 0, fillWidth, _texture.Height);
    //
    //         // Куда рисуем: ту же ширину, но учитываем масштаб
    //         Rectangle destinationRect = new Rectangle(
    //             dimensions.X,
    //             dimensions.Y,
    //             (int)(fillWidth * ImageScale),
    //             (int)(_texture.Height * ImageScale)
    //         );
    //
    //         // Рисуем только кусок текстуры
    //         spriteBatch.Draw(_texture, destinationRect, sourceRect, Color.White);
    //     }
    // }

    
    
    
    //Система для отрисовки UI, происходит только на стороне клиента, так как любая отрисовка только на стороне клиента!!!
    [Autoload(Side = ModSide.Client)]
    internal class OverheatUISystem : ModSystem
    {
        // Интерфейс игрока многослойный, поэтому чтобы нарисовать UI мы создаем еще один слой на котором будет рисоваться шкала
        private UserInterface OverheatUIUserInterface;

        // Наследуем класс нашего UIState, который находится выше
        internal OverheatUI OverheatUI;

        public override void Load()
        {
            // При загрузке мода загружаем наш UiState и UserInterface
            OverheatUI = new();
            OverheatUIUserInterface = new();
            // Так как весь слой существует ради шкалы, то устанавливает состояние слоя на эту шкалу, чтобы слой существовал для нее
            OverheatUIUserInterface.SetState(OverheatUI);
        }

        // Интерфейс обновляет каждый тик, поэтому нам важно обновлять интерфейс каждый тик для взаимодействия и каких-либо анимаций, если будут
        public override void UpdateUI(GameTime gameTime)
        {
            OverheatUIUserInterface?.Update(gameTime);
        }

        // Чтобы встроить наш слой в кучу других слоев мы используем метод модификации слоев интерфейса
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            // Здесь мы ищемм индекс слоя интерфейса в Террарии для ванильных ресурс  баров
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            // Если нашелся (То, чтобы не нашелся такого быть не может)
            if (resourceBarIndex != -1)
            {
                // Вставляем в слои, наш слой
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "SiriusMod: Overheat",
                    // Через delegate рисуем сам интерфейс
                    delegate
                    {
                        OverheatUIUserInterface.Draw(Main.spriteBatch, new GameTime());
                        
                        return true;
                    },
                    // Здесь применяется тип скейла, в нашем случае - скейл интерфейс
                    // Попробуй заменить UI на Game, чтобы посмотреть если твоя плашка апскейлится вместе с игрой, а не интерфейсом
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
