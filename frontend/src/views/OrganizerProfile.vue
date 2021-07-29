<template>
  <div class="profile-organizer">
    <img
      src="../assets/OrganizerProfile/Graphics.png"
      alt="Graphics"
      class="graphics"
    />
    <div class="content">
      <div class="left-part part-half">
        <div class="organizer-card">
          <img
            src="../assets/OrganizerProfile/CardMain.png"
            alt=""
            class="card-img"
          />
          <div class="organizer">
            <div class="organizer-image"></div>
            <div class="organizer-info">
              <div class="text">
                <p>ООО "Яндекс"</p>
                <p>Крупнейшая российская IT-компания</p>
                <p>Активных проектов: <span>83</span></p>
                <p>Текущих ивентов: <span>7</span></p>
              </div>
            </div>
          </div>
        </div>
        <div class="info-card">
          <img
            src="../assets/OrganizerProfile/CardAbout.png"
            alt=""
            class="card-img"
          />
          <p class="title">Информация об организаторе</p>
          <p class="text">
            Опыт работы: 2006-2015 гг., компания «IT-Свет»,
            программист-разработчик. Обязанности: создание программных продуктов
            на языках HTML, Паскаль, Perl, Ассемблер, Delphi, PHP; создание
            цифровых узлов и блок-схем; разработка веб-приложений; cоздание и
            администрирование сайтов.<br /><br />Образование: 2003-2005 гг. –
            Тернопольский технический университет, инженер-программист.<br /><br />Прочие
            навыки:<br />•опыт работы со всеми серверными и десктопными версиями
            Windows и Linux;<br />•знание языков С, Perl, PHP, Object Pascal,
            Basic, Java, Assembler<br />•опыт разработки информационных
            систем;<br />•знание основных современных CMS.
          </p>
        </div>
      </div>
      <div class="right-part part-half">
        <div class="events">
          <p class="title">Ивенты</p>
          <ul id="nav">
            <li><a href="#">Прошедшие</a></li>
            <li><a href="#">Текущие</a></li>
            <li><a href="#">Запланированные</a></li>
            <div id="slider"></div>
          </ul>
          <ListProjects
            v-if="user.developer"
            :cards="user.projectsEvent"
            :statistics="true"
          />
        </div>
        <div class="links">
          <img
            src="../assets/OrganizerProfile/CardLinks.png"
            alt=""
            class="card-img"
          />
          <p class="title">Дополнительная информация</p>
          <ul>
            <li><p>Телефон: +7 888 999 99 99</p></li>
            <li><p>Почта:...</p></li>
          </ul>
          <div class="links-image">
            <img src="../assets/ParticipantProfile/LogoVK.svg" alt="Logo" />
            <img
              src="../assets/ParticipantProfile/LogoTelegram.svg"
              alt="Logo"
            />
            <img
              src="../assets/ParticipantProfile/LogoTwitter.svg"
              alt="Logo"
            />
            <img
              src="../assets/ParticipantProfile/LogoFacebook.svg"
              alt="Logo"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ListEvents from "@/components/ProfileListProjectEvent.vue";

async function getDataUser() {
  const res = await fetch(`http://localhost:5000/api/profile/samjokk`, {
    method: "GET",
    // headers: {
    //     Authorization: token
  });
  return await res;
}

export default {
  data() {
    return {
      user: Object,
      // tagsFirstLine: Array,
      // tagsSecondLine: Array,
      idStatistics: Number,
    };
  },
  components: {
    ListEvents,
  },
  methods: {
    async getData() {
      const res = await getDataUser();
      this.user = await res.json();
      console.log(this.user);
      // if (this.user.tags.length > 5) {
      //   this.tagsFirstLine = this.user.tags.slice(0, 5);
      //   this.tagsSecondLine = this.user.tags.slice(5, this.user.tags.length);
      // } else {
      //   this.tagsFirstLine = this.user.tags;
      // }
    },
  },
  async created() {
    await this.getData();
  },
};
</script>

<style lang="scss">
$main-color: linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
$gradient-card: linear-gradient(
  90deg,
  rgba(73, 73, 73, 100%),
  rgba(103, 103, 103, 10%)
);
$bg: #060606;
$color-card: rgba(33, 33, 33, 0.5);
$color-icon: #c7c7c7;
$text-color: #f8f8ff;

%display-between {
  display: flex;
  justify-content: space-between;
}
%display-center {
  display: flex;
  justify-content: center;
}
%display-around {
  display: flex;
  justify-content: space-around;
}
%display-start {
  display: flex;
  justify-content: flex-start;
}
%display-end {
  display: flex;
  justify-content: flex-end;
}
%textGradient {
  background: $main-color;
  background-clip: text;
  -webkit-text-fill-color: transparent;
}
%p0 {
  p {
    margin: 0;
  }
}
%title {
  height: 80px;
  @extend %display-center;
  align-items: center;
}
@mixin Card($card-width) {
  border-radius: 20px;
  backdrop-filter: blur(15px);
  background: $color-card;
  position: relative;
  .card-img {
    position: absolute;
    width: $card-width;
    z-index: -1;
  }
}
@mixin Raleway($fs) {
  font-size: $fs;
  font-family: Raleway;
}

.profile-organizer {
  @extend %display-center;
  margin-top: 150px;
  .content {
    width: 1520px;
    height: 1279px;
    @extend %display-between;
    .part-half {
      @extend %display-between;
      flex-direction: column;
      align-items: center;
    }
    .left-part {
      .organizer-card {
        width: 700px;
        height: 283px;
        align-items: center;
        @extend %display-end;
        @include Card(700px);
        .organizer {
          width: 650px;
          height: 170px;
          @extend %display-start;
          .organizer-image {
            width: 150px;
            height: 150px;
            border-radius: 100px;
            background: url("../assets/ParticipantProfile/Avatar.jpg");
          }
          .organizer-info {
            width: 420px;
            margin-left: 30px;
            @extend %p0;
            .text {
              @include Raleway(18px);

              p {
                margin-top: 20px;
                span {
                  font-family: OpenSans;
                }
              }
              p:nth-child(1) {
                margin-top: 0px;
                font-size: 24px;
              }
            }
          }
        }
      }
      .info-card {
        width: 700px;
        height: 966px;
        @extend %p0;
        @include Card(700px);
        .title {
          @extend %title;
        }
        .text {
          margin: 20px 50px;
        }
      }
    }
    .right-part {
      width: 790px;
      .events {
        height: 949px;
        @extend %display-between;
        flex-direction: column;
        align-items: center;
        @extend %p0;
        .title {
          @extend %title;
        }
        #nav {
          width: 790px;
          height: 62px;
          @extend %display-between;
          padding: 0;
          position: relative;
          li {
            list-style-type: none;
            float: left;
            align-self: center;
            a {
              @include Raleway(24px);
              @extend %textGradient;
            }
          }
          #slider {
            left: 0px;
            position: absolute;
            left: 197.5px;
            top: 58px;
            height: 2px;
            background: $color-icon;
            width: 260px;
            transition: all 300ms ease;
            opacity: 0;
          }
          li:nth-child(1) {
            margin-left: 60px;
          }
          li:nth-child(2) {
            margin-left: 35px;
          }
          li:nth-child(3) {
            margin-right: 30px;
          }
          li:nth-child(1):hover ~ #slider {
            left: 0px;
            opacity: 1;
          }
          li:nth-child(2):hover ~ #slider {
            left: 260px;
            opacity: 1;
          }
          li:nth-child(3):hover ~ #slider {
            left: 520px;
            opacity: 1;
          }
        }
        #ul-events {
          width: 680px;
          height: 775px;
          padding-left: 30px;
          list-style-type: none;
          overflow: auto;
          &::-webkit-scrollbar {
            width: 10px;
            border-radius: 25px;
          }
          &::-webkit-scrollbar-thumb {
            background: -webkit-linear-gradient(
              319.66deg,
              #20e4ff 19%,
              #8b30ff 75.54%
            );
            box-shadow: inset 2px 1000px 2px rgb(20, 20, 20, 1);
            border: solid 2px transparent;
            background-origin: border-box;
            border-radius: 25px;
          }
          li {
            margin-top: 30px;
          }
        }
      }
      .links {
        @extend %display-between;
        flex-direction: column;
        align-items: center;
        width: 100%;
        height: 300px;
        @extend %p0;
        @include Card(790px);
        .title {
          @extend %title;
        }
        ul {
          margin: 20px 50px;
          align-self: start;
          list-style-type: disc;
          color: $text-color;
        }
        .links-image {
          width: 500px;
          @extend %display-between;
          margin-bottom: 50px;
          img {
            width: 60px;
            height: 60px;
          }
        }
      }
    }
  }

  .title {
    @include Raleway(30px);
  }
  .graphics {
    position: absolute;
    z-index: -1;
    top: 1106px;
    left: 55px;
  }
}
</style>
