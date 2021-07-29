<template>
  <ul id="ul-projects" class="ul-profile-projects-events">
    <li class="li-project" v-for="project in cards" :key="project">
      <div class="mini-card-project">
        <img
          src="../assets/CardProjectEvent/CardProjectMini.png"
          alt="Card"
          class="card-img"
        />
        <div class="upline">
          <img src="../assets/CardProjectEvent/AvatarProject.png" alt="" />
          <div class="uptext">
            <p class="rating">Рейтинг: {{ project.rating }}</p>
            <p v-if="project.organizerName" class="title">
              {{ `${project.organizerName} "${project.name}"` }}
            </p>
            <p v-else class="title">
              {{ `"${project.name}"` }}
            </p>
          </div>
        </div>
        <div class="middleline">
          <p v-if="project.description" class="description">
            {{ `${project.description.substr(0, 250)}...` }}
          </p>
        </div>
        <div class="bottomline">
          <div class="statistics">
            <div class="statistics-item">
              <img
                src="../assets/CardProjectEvent/Participants.svg"
                alt="Participants"
              />
              <p>{{ project.membersCount }}</p>
            </div>
            <div class="statistics-item">
              <img src="../assets/CardProjectEvent/Date.svg" alt="Date" />
              <p>{{ project.creationDate.split("T")[0] }}</p>
            </div>
          </div>
          <router-link to="/">
            <Button :textButton="'Изучить'" :width="'130px'" :height="'41px'" />
          </router-link>
        </div>
      </div>
    </li>
  </ul>
</template>

<script>
import Button from "@/components/Buttons/ButtonProject.vue";
import $ from "jquery";

export default {
  components: {
    Button,
  },
  props: {
    cards: Object,
    statistics: Boolean,
    idList: Number,
  },
  mounted() {
    let ul = document.getElementsByClassName("ul-profile-projects-events");
    let statistics = ul[this.idList].getElementsByClassName(`statistics`);
    if (this.statistics) {
      for (let i = 0; i < this.cards.length; i++) {
        let div = document.createElement("div");
        div.className = "statistics-item";
        let image = document.createElement("div");
        image.className = "image-news";
        let par = document.createElement("p");
        par.innerHTML = this.cards[i].newsCount;
        div.append(image);
        div.append(par);
        $(statistics[i]).prepend(div);
      }
    } else {
      for (let i = 0; i < this.cards.length; i++) {
        let div = document.createElement("div");
        div.className = "statistics-item";
        let image = document.createElement("div");
        image.className = "image-projects";
        let par = document.createElement("p");
        par.innerHTML = this.cards[i].newsCount;
        div.append(image);
        div.append(par);
        $(statistics[i]).prepend(div);
      }
    }

    // console.log(statistics.firstElementChild);
  },
};
</script>

<style lang="scss">
$color-card: rgba(33, 33, 33, 0.5);

%display-between {
  display: flex;
  justify-content: space-between;
}

#ul-projects {
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
    background: -webkit-linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
    box-shadow: inset 2px 1000px 2px rgb(20, 20, 20, 1);
    border: solid 2px transparent;
    background-origin: border-box;
    border-radius: 25px;
  }
  li {
    margin-top: 30px;
  }
}

.mini-card-project {
  width: 650px;
  height: 280px;
  @extend %display-between;
  flex-direction: column;
  align-items: center;
  border-radius: 20px;
  backdrop-filter: blur(15px);
  background: $color-card;
  position: relative;
  font-size: 14px;
  .card-img {
    position: absolute;
    z-index: -1;
  }
  .upline {
    @extend %display-between;
    padding: 30px 0px 0px 15px;
    .rating {
      display: flex;
      justify-content: flex-end;
    }
    .title {
      width: 430px;
      padding-left: 20px;
      font-size: 18px;
      font-family: Raleway;
      display: flex;
      justify-content: center;
      align-items: center;
      text-align: center;
    }
  }
  .middleline {
    width: 580px;
    height: 75px; //БЫЛО 80
    display: flex;
    justify-content: center;
    align-items: center;
    overflow: hidden;
    //   .description {
    //   }
  }
  .bottomline {
    @extend %display-between;
    width: 580px;
    height: 65px;
    .statistics {
      @extend %display-between;
      .statistics-item {
        display: flex;
        justify-content: flex-start;
        flex-direction: column;
        align-items: center;
        .image-projects {
          width: 33px;
          height: 30px;
          background: url("../assets/CardProjectEvent/Projects.svg");
        }
        .image-news {
          width: 28px;
          height: 28px;
          background: url("../assets/CardProjectEvent/News.svg");
        }
        img {
          height: 28px;
        }
        p {
          width: 75px;
          height: 30px;
          font-size: 12px;
          text-align: center;
        }
      }
    }
  }
}
</style>