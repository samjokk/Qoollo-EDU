<template>
  <ul id="ul-projects-events">
    <li
      class="li-project-event"
      v-for="projectEvent in cards"
      :key="projectEvent"
    >
      <div class="normal-card-project-event">
        <img
          src="../assets/CardProjectEvent/CardProject.png"
          alt="Card"
          class="card-img"
        />
        <div class="upline">
          <img src="../assets/CardProjectEvent/AvatarProject.png" alt="" />
          <div class="uptext">
            <p class="rating">Рейтинг: {{ projectEvent.rating }}</p>
            <p v-if="projectEvent.organizerName" class="title title-card-project">
              {{ `${projectEvent.organizerName} "${projectEvent.name}"` }}
            </p>
            <p v-else class="title title-card-project">
              {{ projectEvent.name }}
            </p>
            <div class="tags-project-event">
              <p v-for="tag in projectEvent.tags" :key="tag">{{ tag.name }}</p>
            </div>
          </div>
        </div>
        <div class="middleline">
          <p class="description">
            {{ projectEvent.description }}
          </p>
        </div>
        <div class="bottomline">
          <div class="statistics">
            <div class="statistics-item">
              <img
                src="../assets/CardProjectEvent/Participants.svg"
                alt="Participants"
              />
              <p>{{ projectEvent.membersCount }}</p>
            </div>
            <div class="statistics-item">
              <img src="../assets/CardProjectEvent/Date.svg" alt="Date" />
              <p v-if="projectEvent.creationDate">
                {{ projectEvent.creationDate.split("T")[0] }}
              </p>
              <p v-else>
                {{
                  `${projectEvent.startDate.split("T")[0]}/ ${
                    projectEvent.endDate.split("T")[0]
                  }`
                }}
              </p>
            </div>
          </div>
          <router-link
            v-if="projectEventFlag"
            :to="'/project/' + projectEvent.id"
          >
            <Button :textButton="'Изучить'" :width="'130px'" :height="'41px'" />
          </router-link>
          <router-link v-else :to="'/event/' + projectEvent.id">
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
    projectEventFlag: Boolean,
  },
  async mounted() {
    let statistics = await document.getElementsByClassName(`statistics`);
    if (statistics.length != 0) {
      for (let i = 0; i < this.cards.length; i++) {
        let div = document.createElement("div");
        div.className = "statistics-item";
        let image = document.createElement("div");
        let par = document.createElement("p");
        if (this.projectEventFlag) {
          image.className = "image-news";
          par.innerHTML = this.cards[i].newsCount;
        } else {
          image.className = "image-projects";
          par.innerHTML = this.cards[i].projectCount;
        }
        div.append(image);
        div.append(par);
        $(statistics[i]).prepend(div);
      }
    }
    let tags = document.getElementsByClassName("tags-project-event");
    for (let i = 0; i < tags.length; i++) {
      let tagsProject = tags[i].querySelectorAll("p");
      for (let j = 0; j < tagsProject.length; j++) {
        tagsProject[j].style.color = this.cards[i].tags[j].color;
      }
    }
    // console.log(a[0]);
  },
};
</script>

<style lang="scss">
$color-card: rgba(33, 33, 33, 0.5);

%display-between {
  display: flex;
  justify-content: space-between;
}

%display-around {
  display: flex;
  justify-content: space-around;
}

#ul-projects-events {
  width: 1030px;
  height: 2118px;
  padding-left: 30px;
  list-style-type: none;
  overflow: auto;
  &::-webkit-scrollbar {
    width: 10px;
    border-radius: 25px;
  }
  &::-webkit-scrollbar-thumb {
    background: -webkit-linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
    box-shadow: inset 2px 10000px 2px rgb(20, 20, 20, 1);
    border: solid 2px transparent;
    background-origin: border-box;
    border-radius: 25px;
  }
  li {
    margin-top: 30px;
  }
}

.normal-card-project-event {
  width: 1000px;
  height: 500px;
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
      height: 20px;
      display: flex;
      justify-content: flex-end;
    }
    .title {
      width: 750px;
      height: 40px;
      padding-left: 20px;
      font-size: 18px;
      font-family: Raleway;
      display: flex;
      justify-content: center;
      align-items: center;
      text-align: center;
    }
    .tags-project-event {
      width: 780px;
      height: 30px;
      @extend %display-around;
      margin-top: 10px;
    }
  }
  .middleline {
    width: 900px;
    height: 250px;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  .bottomline {
    @extend %display-between;
    width: 900px;
    height: 70px;
    .statistics {
      @extend %display-between;
      .statistics-item {
        display: flex;
        justify-content: flex-start;
        flex-direction: column;
        align-items: center;
        .image-projects {
          width: 34px;
          height: 28px;
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
  p {
    margin: 0;
  }
}
</style>