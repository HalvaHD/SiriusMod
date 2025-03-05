using SiriusMod.Content.Items;
using SiriusMod.Mechanics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ReLogic.Content;
using SiriusMod.Content.Items.Tools.PreHM.PathfinderPickaxe;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Animations;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace SiriusMod.UI.Mechanics;

internal class OverheatUI : UIState
{
    private UIElement area;
    private UIImage frame;
    private UIImage progressBar;

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
        frame.Width.Set(36, 2f);
        frame.Height.Set(12, 2f);
        
        progressBar = new UIImage(ModContent.Request<Texture2D>("SiriusMod/Assets/ExtraTextures/Kitkat_Bar"));
        
        //К основной области добавляешь все элементы, которые должны "крепиться" к ней
        area.Append(frame);
        // area.Append(progressBar);
        // Добавляешь саму область на экран
        Append(area);
    }

    // Отрисовка UI, чтобы ее отменить возращаем return;
    public override void Draw(SpriteBatch spriteBatch)
    { 
        if (Main.LocalPlayer.HeldItem.ModItem is not Overheat)  // && !Main.LocalPlayer.controlUseItem
        {
            return;
        }
        
        base.Draw(spriteBatch);
    }

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
                    "ExampleMod: Example Resource Bar",
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
