<template>
  <div class="home">
    <img
      src="../assets/Landing/GraphicsUp.png"
      alt="graphics up"
      class="graphics graphics-up"
    />
    <img
      src="../assets/Landing/GraphicsMiddle.png"
      alt="GraphicsMiddle"
      class="graphics graphics-middle"
    />
    <img
      src="../assets/Landing/GraphicsBelow.png"
      alt="graphics below"
      class="graphics graphics-below"
    />
    <div class="title anim-items anim-no-hide">
      <div class="logo anim-show-text">
        <p>QOOLLO</p>
        <p class="EDU">EDU</p>
      </div>
      <p class="text anim-show-text text-title">
        Современная IT-платформа для создания, развития, ведения и презентации
        своих проектов
      </p>
    </div>
    <div class="container">
      <div class="container-up">
        <div class="left-text anim-items">
          <SmallCardInfo
            class="anim-show-card-left"
            :title="'Придумай идею'"
            :text="'Идея является точкой старта для любого IT-проекта, поэтому в самом начале определи, что ты хочешь создать, кому это будет интересно и как бы это могло выглядеть.'"
          />
          <SmallCardInfo
            class="down-card-text anim-show-card-left"
            :title="'Собери команду'"
            :text="'Когда ты уже наметил свою деятельность, надо подумать, сколько рук для этого потребуется. Находи разноплановых людей, которым нравится твоя идея, чтобы синергитический эффект сделал своё дело.'"
          />
        </div>

        <CardsImage />

        <div class="right-text anim-items">
          <SmallCardInfo
            class="anim-show-card-right"
            :title="'Разработай'"
            :text="'Люди есть, планы тоже - теперь пора работать. Декомпозируйте задачи в своей команде, распределите роли, определите технологический стек, временные спринты, и приступайте за дело.'"
          />
          <SmallCardInfo
            class="down-card-text anim-show-card-right"
            :title="'Презентуй'"
            :text="'Ваш проект готов, вы довольны своим результатом, и теперь следует рассказать о нём всему миру. Публикуйте проект на нашей площадке и ждите наилучший отзывов о нём!'"
          />
        </div>
      </div>

      <div class="container-middle">
        <div class="container-events container-middle-part">
          <div class="container-text-info anim-items">
            <p class="text-header anim-show-text">События (Events)</p>
            <p class="text-info anim-show-text">
              Событие - промежуток времени с обозначенной тематикой, в рамках
              которой команды разрабатывают свои IT-проекты. Событие запускается
              организаторами-компаниями чтобы достигнуть желаемых целей.<br /><br />Событие
              может быть трёх видов:<br />• Запланированное;<br />• Текущее;<br />•
              Прошедшее.<br /><br />Чтобы принять участие в событии, необходимо
              разыскать единомышленников и зарегистрировать свой IT-проект.
              После завершения проектов лучшие из них вознаграждаются!
            </p>
            <router-link class="button-link" to="/eventspage">
              <Button class="button" :textButton="'Посмотреть события'" />
            </router-link>
          </div>
          <div class="container-image anim-items">
            <img
              class="anim-show-circle"
              id="large-circle"
              src="../assets/Landing/LargeCircle.svg"
              alt="large circle"
            />
            <img
              class="anim-show-circle"
              id="small-circle"
              src="../assets/Landing/SmallCircle.svg"
              alt="small circle"
            />
          </div>
        </div>

        <div class="container-projects container-middle-part">
          <div class="container-image anim-items">
            <div
              class="card-info-projects card-info-projects-first anim-show-card-projects"
            >
              <p>Придумай</p>
              <img src="../assets/Landing/CardLamp.png" alt="large circle" />
            </div>
            <div
              class="card-info-projects card-info-projects-second anim-show-card-projects"
            >
              <p>Собери команду</p>
              <img src="../assets/Landing/CardTeam.png" alt="small circle" />
            </div>
            <div
              class="card-info-projects card-info-projects-third anim-show-card-projects"
            >
              <p>Разработай</p>
              <img src="../assets/Landing/CardDevelop.png" alt="small circle" />
            </div>
          </div>
          <div class="container-text-info anim-items">
            <p class="text-header anim-show-text">IT-проекты</p>
            <p class="text-info anim-show-text">
              IT-проект - это дело, которым занимается команда, исходя из
              учебных, коммерческих или личных целей. Каждый участник может
              существовать сразу в нескольких IT-проектах, которые в свою
              очередь делятся на:<br />
              • состоящие в ивенте;<br />• самостоятельные.<br /><br />Чтобы
              принять участие в проекте, желательно собрать команду (это можно
              сделать как самостоятельно, так и в рамках нашей платформы в
              разделе УЧАСТНИКИ), после чего нужно зарегестрировать ваш
              IT-проект и, конечно же, начать разработку.
            </p>
            <router-link class="button-link" to="/projectspage">
              <Button class="button" :textButton="'Посмотреть проекты'" />
            </router-link>
          </div>
        </div>

        <div class="container-participants container-middle-part">
          <div class="container-text-info anim-items">
            <p class="text-header anim-show-text">Участники</p>
            <p class="text-info anim-show-text">
              Участник - человек, состоящий в команде и разрабатывающий со своей
              командой проекты. Будучи участником, вы можете:<br />• Принимать
              участие в ивентах;<br />• Принимать участие в существующих
              проектах;<br />• Создавать собственные проекты;<br />• Вести
              страницы своих проектов;<br />• Следить за другими проектами;<br />•
              Зарабатывать рейтинг для продвижения своего профиля.<br /><br />Чтобы
              стать участником, необходимо всего лишь зарегистрироваться на
              нашем портале Qoollo EDU!
            </p>
            <router-link class="button-link" to="/users">
              <Button class="button" :textButton="'Найти участников'" />
            </router-link>
          </div>
          <div class="container-image-participants anim-items">
            <SmallCardInfoParticipants
              id="card-first"
              class="card anim-show-card-participants"
              :textCard="'Совершенствуйся'"
            />
            <SmallCardInfoParticipants
              id="card-second"
              class="card anim-show-card-participants"
              :textCard="'Создавай'"
            />
            <SmallCardInfoParticipants
              id="card-third"
              class="card anim-show-card-participants"
              :textCard="'Развивайся'"
            />
            <div class="gear anim-show-card-participants"></div>
          </div>
        </div>

        <div class="container-organizers container-middle-part">
          <div class="image anim-items anim-show-card-organizers">
            <img src="../assets/Landing/Organizers.png" alt="organizers" />
          </div>
          <div class="container-text-info anim-items">
            <p class="text-header anim-show-text">Организаторы</p>
            <p class="text-info anim-show-text">
              Организатор - организация или человек, которые запускают своим
              события, и тем самым дают возможность разработчикам (участникам)
              создавать свои проекты. Организатор может:<br />• Проводить сразу
              несколько событий;<br />• Самостоятельно искать людей на своё
              событие; <br />• Публиковать информацию по своим событиям для
              проектов, участвующих в них;<br />• Награждать участников за
              участие в событиях.<br /><br />Чтобы стать организатором,
              необходимо зарегистрироваться на нашем портале Qoollo EDU как
              организатор и подать заявку на утверждение!
            </p>
            <router-link class="button-link" to="/users">
              <Button class="button" :textButton="'Пригласить участников'" />
            </router-link>
          </div>
        </div>
      </div>

      <div class="container-down">
        <div class="left-text text-block">
          <p>Остались вопросы?</p>
        </div>
        <img src="../assets/Landing/Line.png" alt="line" class="image-line" />
        <div class="right-text text-block">
          <p>Свяжитесь с нами и мы поможем вам освоить нашу платформу!</p>
          <div class="contact-link">
            <p>Telegram</p>
            <img src="../assets/Landing/ArrowLink.svg" alt="line" />
          </div>
          <div class="contact-link">
            <p>Вконтакте</p>
            <img src="../assets/Landing/ArrowLink.svg" alt="line" />
          </div>
          <div class="contact-link">
            <p>WhatsApp</p>
            <img src="../assets/Landing/ArrowLink.svg" alt="line" />
          </div>
        </div>
      </div>
    </div>
    <Footer />
  </div>
</template>

<script>
import Header from "@/components/Header.vue";
import SmallCardInfo from "@/components/Cards/SmallCardInfo.vue";
import CardsImage from "@/components/Cards/CardsImage.vue";
import Button from "@/components/Buttons/ButtonLanding.vue";
import SmallCardInfoParticipants from "@/components/Cards/SmallCardInfoParticipants.vue";
import Footer from "@/components/Footer.vue";

function animationText() {
  const animItems = document.querySelectorAll(".anim-items");
  if (animItems.length > 0) {
    window.addEventListener("scroll", animOnScroll);
    animOnScroll(animItems);
  }
}
function animOnScroll(animItems) {
  for (let index = 0; index < animItems.length; index++) {
    const animItem = animItems[index];
    const animItemHeight = animItem.offsetHeight; //Высота объекта
    const animItemOffsetTop = getOffset(animItem); //Позиция объекта по вертикали
    const animStart = 2;

    let animItemPoint = window.innerHeight - animItemHeight / animStart;
    if (animItemPoint > window.innerHeight) {
      animItemPoint = window.innerHeight - window.innerHeight / animStart;
    }
    if (
      pageYOffset > animItemOffsetTop - animItemPoint &&
      pageYOffset < animItemOffsetTop + animItemHeight
    ) {
      animItem.classList.add("active");
    } else {
      if (!animItem.classList.contains("anim-no-hide"))
        animItem.classList.remove("active");
    }
  }
}
function getOffset(el) {
  const rect = el.getBoundingClientRect(),
    scrollTop = window.pageYOffset || document.documentElement.scrollTop;
  return rect.top + scrollTop;
}

export default {
  data() {
    return {};
  },
  components: {
    Header,
    SmallCardInfo,
    CardsImage,
    Button,
    SmallCardInfoParticipants,
    Footer,
  },
  mounted() {
    this.$root.$refs.header.printMenu(0);
    window.addEventListener("scroll", this.onScroll);
    const animItems = document.querySelectorAll(".anim-items");
    setTimeout(() => {
      animOnScroll(animItems);
    }, 500);
  },
  methods: {
    async onScroll() {
      window.onscroll = await animationText();
    },
  },
};
</script>

<style lang="scss">
$gradient: linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
$bg-color: #060606;
$animationNormalTime: 1s;
$animationCircleTime: 1.5s;
$animationCardProjectsTime: 0.3s;
$animationBlockDelay: 0.5s;
.home {
  overflow: hidden;
  .title {
    .logo {
      display: flex;
      margin: 140px 0px 0px 100px;
      .EDU {
        margin-left: 30px;
        background: $gradient;
        background-clip: text;
        -webkit-text-fill-color: transparent;
      }
      p {
        font-size: 64px;
        font-family: Raleway;
      }
    }
    .text {
      height: 40px;
      margin: 100px 0px 0px 200px;
      font-size: 24px;
    }
  }
  p {
    margin: 0;
    font-size: 18px;
  }

  .container {
    display: flex;
    flex-direction: column;
    margin: 630px 100px 100px 100px;
    .container-up {
      margin-top: 100px;
      border: 0px solid #ffffff;
      height: 700px;
      display: flex;
      justify-content: space-between;
      .down-card-text {
        margin-top: 140px;
      }
      .right-text {
        text-align: right;
      }
    }
    .container-middle {
      margin: 0px 200px 0px 200px;
      .container-middle-part {
        margin-top: 350px;
        display: flex;
        justify-content: space-between;
        .container-text-info {
          width: 723px;
          display: flex;
          flex-direction: column;
          p {
            overflow: hidden;
          }
          .text-header {
            height: 36px;
            font-family: Raleway;
            font-size: 36px;
            text-align: center;
            padding: 22px 0px 22px 0px;
          }
          .text-info {
            margin: 30px 0px 90px 0px;
          }
          .button-link {
            align-self: flex-end;
            width: 296px;
            height: 47px;
          }
          .button {
            width: 296px;
            height: 47px;
          }
        }
      }
      .container-events {
        .container-image {
          width: 454px;
          height: 454px;
          #large-circle {
            position: absolute;
          }
          #small-circle {
            position: relative;
            top: 85px;
            left: 90px;
          }
        }
      }
      .container-projects {
        .container-image {
          .card-info-projects {
            display: flex;
            justify-content: space-between;
            width: 414px !important;
            height: 100px;
            margin-top: 50px;
            border: solid 3px transparent;
            background-origin: border-box;
            background-image: $gradient;
            border-radius: 10px;
            box-shadow: inset 2px 1000px 1px $bg-color;
            P {
              margin-left: 20px;
              font-family: Raleway;
              font-size: 24px;
              height: 30px;
              align-self: center;
            }
          }
        }
      }
      .container-participants {
        .container-image-participants {
          width: 538px;
          height: 460px;
          position: relative;
          .card {
            position: absolute;
          }
          .gear {
            width: 98px;
            height: 98px;
            position: absolute;
            top: 181px;
            left: 219px;
            background-image: url("../assets/Landing/Gear.svg");
          }
          #card-first {
            top: 150px;
            left: -60px;
          }
          #card-second {
            top: 150px;
            left: 220px;
          }
          #card-third {
            top: 373px;
            left: 79px;
          }
        }
      }
      .container-organizers {
        .image {
          width: 454px;
          height: 454px;
          border-radius: 250px;
          background-color: rgba(39, 39, 47, 0.4);
          backdrop-filter: blur(20px);
          background: url("../assets/Landing/SmallCardBackgroundOrganizers.png");
          display: flex;
          justify-content: center;
          align-items: center;
          transform-origin: left center;
        }
      }
    }
    .container-down {
      height: 454px;
      display: flex;
      justify-content: space-around;
      align-items: center;
      margin: 750px 403.5px 0px 403.5px;
      border-radius: 25px;
      background-color: rgba(39, 39, 47, 0.4);
      backdrop-filter: blur(20px);
      background: url("../assets/Landing/CardBackgroundInfoBottom.png");
      .text-block {
        width: 400px;
        height: 333px;
      }
      .left-text {
        p {
          font-size: 36px;
          font-family: Raleway;
          text-align: center;
        }
      }
      .right-text {
        display: flex;
        flex-direction: column;
        justify-content: flex-end;
        .contact-link {
          display: flex;
          justify-content: space-between;
          margin-top: 22px;
          width: 173px;
          height: 40px;
          p {
            font-size: 24px;
          }
        }
      }
      .image-line {
        height: 376px;
      }
    }
  }

  .graphics {
    position: absolute;
    z-index: -1;
  }

  .graphics-up {
    top: 1812px;
  }

  .graphics-middle {
    top: 1838px;
    left: 1555px;
  }
  .graphics-below {
    top: 5290px;
    left: 0px;
  }

  .anim-show-text {
    opacity: 0;
    transform: translate(0px, 120%);
    transition: all $animationNormalTime ease 0.1s;
  }
  .anim-show-text.text-title {
    transition: all $animationNormalTime ease 0.4s;
  }
  .anim-show-text.active,
  .active .anim-show-text {
    opacity: 1;
    transform: translate(0px, 0px);
  }

  .anim-show-card-left {
    opacity: 0;
    transform-origin: left bottom;
    transform: rotate(-180deg);
    transition: all $animationNormalTime ease 0.1s;
  }
  .anim-show-card-left.down-card-text {
    transform: rotate(180deg);
    transform-origin: left top;
  }
  .anim-show-card-left.active,
  .active .anim-show-card-left {
    opacity: 1;
    transform: rotate(0deg);
  }
  .anim-show-card-right {
    opacity: 0;
    transform-origin: right bottom;
    transform: rotate(180deg);
    transition: all $animationNormalTime ease 0.1s;
  }
  .anim-show-card-right.down-card-text {
    transform-origin: right top;
    transform: rotate(-180deg);
  }
  .anim-show-card-right.active,
  .active .anim-show-card-right {
    opacity: 1;
    transform: rotate(0deg);
  }

  .anim-show-circle {
    opacity: 0;
    transition: all $animationCircleTime ease $animationBlockDelay;
    transform: translate(240px, 0px) rotate(-180deg);
  }
  .anim-show-circle.active,
  .active .anim-show-circle {
    opacity: 1;
    transform: translate(0px, 0px) rotate(0deg);
  }
  .anim-show-circle#small-circle.active,
  .active .anim-show-circle#small-circle {
    animation: small-circle-scale 10s linear infinite
      ($animationCircleTime + $animationBlockDelay);
  }
  .anim-show-circle#large-circle.active,
  .active .anim-show-circle#large-circle {
    animation: large-circle-scale 10s linear infinite
      ($animationCircleTime + $animationBlockDelay);
  }

  .anim-show-card-projects {
    opacity: 0;
    transition: all $animationCardProjectsTime ease $animationBlockDelay;
    transform: rotate(-90deg);
  }
  .anim-show-card-projects.active,
  .active .anim-show-card-projects {
    opacity: 1;
    transform: rotate(0deg);
  }
  .anim-show-card-projects.card-info-projects-first.active,
  .active .anim-show-card-projects.card-info-projects-first {
    transition: all $animationCardProjectsTime ease
      ($animationBlockDelay + $animationCardProjectsTime * 2);
  }
  .anim-show-card-projects.card-info-projects-second.active,
  .active .anim-show-card-projects.card-info-projects-second {
    transition: all $animationCardProjectsTime ease
      ($animationBlockDelay + $animationCardProjectsTime);
  }

  .anim-show-card-participants {
    opacity: 0;
    transition: all $animationNormalTime ease $animationBlockDelay;
  }
  .anim-show-card-participants#card-first {
    transform: translate(-150px, 150px);
  }
  .anim-show-card-participants#card-second {
    transform: translate(150px, 150px);
  }
  .anim-show-card-participants#card-third {
    transform: translate(0px, 150px);
  }
  .anim-show-card-participants.active,
  .active .anim-show-card-participants {
    opacity: 1;
    transform: translate(0px, 0px);
  }
  .anim-show-card-participants#card-first.active,
  .active .anim-show-card-participants#card-first {
    transform: matrix(0.5, -0.87, 0.86, 0.5, 0, 0);
  }
  .anim-show-card-participants#card-second.active,
  .active .anim-show-card-participants#card-second {
    transform: matrix(0.5, 0.86, -0.87, 0.49, 0, 0);
  }
  .anim-show-card-participants#card-third.active,
  .active .anim-show-card-participants#card-third {
    transform: translate(0px, 0px);
  }
  .anim-show-card-participants.gear.active,
  .active .anim-show-card-participants.gear {
    animation: rotate360deg 3s linear infinite 1s + $animationBlockDelay;
  }

  .anim-show-card-organizers {
    opacity: 0;
    transition: all $animationNormalTime ease $animationBlockDelay;
    transform: rotate(-180deg);
  }
  .anim-show-card-organizers.active,
  .active .anim-show-card-organizers {
    opacity: 1;
    transform: rotate(0deg);
  }
}

@keyframes large-circle-scale {
  0% {
    transform: scale(1);
  }
  25% {
    transform: scale(0.9);
  }
  50% {
    transform: scale(1);
  }
  75% {
    transform: scale(1.1);
  }
  100% {
    transform: scale(1);
  }
}

@keyframes small-circle-scale {
  0% {
    transform: scale(1);
  }
  25% {
    transform: scale(1.1);
  }
  75% {
    transform: scale(0.9);
  }
  100% {
    transform: scale(1);
  }
}

@keyframes rotate360deg {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>
