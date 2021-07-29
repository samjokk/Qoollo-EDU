<template>
  <div class="events-page">
    <img
      src="../assets/ProjectsEventsPage/Graphics.png"
      alt="Graphics"
      class="graphics"
    />
    <div class="content">
      <div class="best-events">
        <img
          src="../assets/ProjectsEventsPage/TopProjectsEvents.png"
          alt="Card"
          class="card-img"
        />
        <TopEvents
          :cards="topEvents"
          :projectEventFlag="false"
          class="best-events-list"
        />
      </div>
      <div id="events-list" class="events">
        <div class="search events-item">
          <p>Найди своё событие и собери для него проектную команду!</p>
          <div class="searchline">
            <img src="../assets/ProjectsEventsPage/Search.svg" alt="Search" />
            <label class="searchlinelabel">
              <input
                name="searchline-filter"
                type="text"
                placeholder="Найти событие"
                v-on:keyup.enter="findEvent($event)"
            /></label>
          </div>
        </div>
        <ListEvents
          v-if="events[0]"
          :cards="events"
          :statistics="false"
          class="eventsList events-item"
        />
      </div>
      <div class="filters">
        <div class="menu-filters" @click="showMenu">
          <p>Фильтры</p>
          <div class="arrow-filters"></div>
        </div>
        <div class="block-filters">
          <div class="title">
            <p>Фильтры</p>
          </div>
          <div class="tags">
            <div class="title">
              <img src="../assets/ProjectsEventsPage/Search.svg" alt="" />
              <p>Теги</p>
            </div>
            <label class="searchline">
              <input
                type="text"
                v-if="tags.length"
                placeholder="Найти тег"
                v-on:keyup="findTag($event)"
                @click="getListTags"
            /></label>
            <ul id="ul-tags-hint">
              <li
                class="li-tags-hint"
                v-for="tag in tags"
                :key="tag"
                @click="addTag(tag)"
              >
                {{ tag.name }}
              </li>
            </ul>
            <div class="container-tags">
              <ul id="container-tags-first"></ul>
              <ul id="container-tags-second"></ul>
            </div>
          </div>
          <div class="rating">
            <p class="title">Рейтинг(лайки)</p>
            <div class="order">
              <label class="radio">
                <input type="radio" name="likes" value="ascending" />
                <div class="radio-text">По возрастанию</div>
              </label>
              <label class="radio">
                <input type="radio" name="likes" value="decreasing" />
                <div class="radio-text">По убыванию</div>
              </label>
            </div>
            <Button
              class="button-get-filters"
              :textButton="'Применить фильтры'"
              :width="'220px'"
              :height="'50px'"
              @click="getFilters"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import TopEvents from "@/components/Cards/TopProjectsEvents.vue";
import ListEvents from "@/components/ListProjectEvent.vue";
import Button from "@/components/Buttons/ButtonProject.vue";

let checkEvents = true;
let allEventsHTML;
let copyAllEventsHTML;

async function getDataEvent() {
  const res = await fetch(`http://localhost:5000/api/search/events`, {
    method: "GET",
    // headers: {
    //     Authorization: token
  });
  return await res;
}

export default {
  data() {
    return {
      events: Object,
      tags: Object,
      topEvents: Object,
    };
  },
  components: {
    TopEvents,
    ListEvents,
    Button,
  },
  methods: {
    showMenu() {
      let menu = document.getElementsByClassName("block-filters");
      let arrow = document.getElementsByClassName("arrow-filters");
      if (
        !menu[0].classList.contains("block-filter-visibal") &&
        !arrow[0].classList.contains("arrow-rotate")
      ) {
        menu[0].classList.add("block-filter-visibal");
        arrow[0].classList.add("arrow-rotate");
      } else {
        menu[0].classList.remove("block-filter-visibal");
        arrow[0].classList.remove("arrow-rotate");
      }
    },
    getFilters() {
      this.checkAllEventsHTML();
      let ulFirst = document
        .getElementById("container-tags-first")
        .querySelectorAll("li");
      let ulSecond = document
        .getElementById("container-tags-second")
        .querySelectorAll("li");
      let tags = new Array();
      for (let i = 0; i < ulFirst.length; i++) {
        tags.push(+ulFirst[i].id.substr(1));
      }
      for (let i = 0; i < ulSecond.length; i++) {
        tags.push(+ulSecond[i].id.substr(1));
      }

      let filterLikes = document.getElementsByName("likes");
      let filterEvent = document.getElementsByName("event"); //Поле пока не используется
      //Проверка общая - хз понадобится ли
      // let radioChecked = false;
      // for (let i = 0; i < filtersLikes.length; i++) {
      //   if (filtersLikes[i].checked || filtersEvent[i].checked)
      //     radioChecked = true;
      // }

      allEventsHTML = copyAllEventsHTML;
      this.showAllEvents();
      if (tags.length != 0) this.sortEventsByTags(tags);
      else this.showAllEvents();

      if (filterLikes[0].checked) this.sortEventsAscendingDescending(true);
      else if (filterLikes[1].checked)
        this.sortEventsAscendingDescending(false);
    },
    findEvent(event) {
      this.checkAllEventsHTML();
      if (event.target.value != "") {
        for (let li in this.events) {
          if (
            !this.events[li].name
              .toLowerCase()
              .includes(event.target.value.toLowerCase()) &&
            !allEventsHTML[li].classList.contains("hidden")
          ) {
            allEventsHTML[li].classList.add("hidden");
          } else if (
            this.events[li].name
              .toLowerCase()
              .includes(event.target.value.toLowerCase()) &&
            allEventsHTML[li].classList.contains("hidden")
          ) {
            allEventsHTML[li].classList.remove("hidden");
          }
        }
      } else {
        this.showAllEvents();
      }
    },
    getListTags() {
      let ul = document.getElementById("ul-tags-hint");
      if (!ul.classList.contains("visible")) ul.classList.add("visible");
      else ul.classList.remove("visible");
    },
    findTag(event) {
      let listLi = document.getElementsByClassName("li-tags-hint");
      for (let li in listLi) {
        if (listLi[li].innerHTML) {
          listLi[li].classList.add("hidden");
          if (
            listLi[li].innerHTML
              .toLowerCase()
              .includes(event.target.value.toLowerCase())
          )
            listLi[li].classList.remove("hidden");
        }
      }
    },
    addTag(tag) {
      let ulFirst = document.getElementById("container-tags-first");
      let ulSecond = document.getElementById("container-tags-second");
      let needAdd = true;

      let listLiFirst = ulFirst.querySelectorAll("li");
      let listLiSecond = ulSecond.querySelectorAll("li");
      for (let i = 0; i < listLiFirst.length; i++) {
        if (tag.name === listLiFirst[i].innerText) needAdd = false;
      }
      for (let i = 0; i < listLiSecond.length; i++) {
        if (tag.name === listLiSecond[i].innerText) needAdd = false;
      }

      if (needAdd) {
        let li = document.createElement("li");
        li.innerHTML = tag.name;
        li.style.color = tag.color;
        li.addEventListener("click", this.deleteTag);
        li.click.bind("this");
        li.id = `i${tag.id}`;
        if (ulFirst.getElementsByTagName("li").length < 5) ulFirst.append(li);
        else if (ulSecond.getElementsByTagName("li").length < 5)
          ulSecond.append(li);
      }
    },
    deleteTag(th) {
      let ulFirst = document.getElementById("container-tags-first");
      let ulSecond = document.getElementById("container-tags-second");
      if (ulFirst.querySelector(`#${th.target.id}`))
        ulFirst.removeChild(ulFirst.querySelector(`#${th.target.id}`));
      else ulSecond.removeChild(ulSecond.querySelector(`#${th.target.id}`));
    },
    getHidden() {
      let ul = document.getElementById("ul-tags-hint");
      if (ul.classList.contains("visible")) ul.classList.remove("visible");
    },
    sortEventsByTags(tags) {
      for (let i = 0; i < allEventsHTML.length; i++) {
        let tagsEvent = new Array();
        for (let tag in this.events[i].tags) {
          tagsEvent.push(this.events[i].tags[tag].id);
        }
        if (!this.checkConvergenceTags(tags, tagsEvent)) {
          if (!allEventsHTML[i].classList.contains("hidden"))
            allEventsHTML[i].classList.add("hidden");
        }
      }
    },
    sortEventsAscendingDescending(ascOrDes) {
      let sortedEventsHTML = document.getElementsByClassName("eventsList")[0];
      let listRating = new Array();
      let listId = new Array();

      for (let i = 0; i < allEventsHTML.length; i++) {
        listId.push(i);
        listRating.push(this.events[i].rating);
        sortedEventsHTML.removeChild(allEventsHTML[i]);
      }

      //Получаем 2 массива по возрастанию
      for (let j = 0; j < listRating.length - 1; j++)
        for (let i = 0; i < listRating.length - 1 - j; i++)
          if (listRating[i] >= listRating[i + 1]) {
            [listRating[i], listRating[i + 1]] = [
              listRating[i + 1],
              listRating[i],
            ];
            [listId[i], listId[i + 1]] = [listId[i + 1], listId[i]];
          }
      if (ascOrDes)
        for (let i = 0; i < listId.length; i++)
          sortedEventsHTML.append(allEventsHTML[listId[i]]);
      else if (!ascOrDes)
        for (let i = 0; i < listId.length; i++)
          sortedEventsHTML.prepend(allEventsHTML[listId[i]]);

      allEventsHTML = sortedEventsHTML.querySelectorAll("li");
    },
    checkAllEventsHTML() {
      if (checkEvents) {
        allEventsHTML = document
          .getElementsByClassName("eventsList")[0]
          .querySelectorAll("li");
        copyAllEventsHTML = allEventsHTML;
        checkEvents = false;
      }
    },
    showAllEvents() {
      for (let li in this.events)
        if (allEventsHTML[li].classList.contains("hidden"))
          allEventsHTML[li].classList.remove("hidden");
    },
    checkConvergenceTags(tags, tagsEvent) {
      let convergence = true;
      for (let j = 0; j < tags.length; j++) {
        if (!tagsEvent.includes(tags[j])) convergence = false;
      }
      return convergence;
    },
    async getData() {
      let res = await getDataEvent();
      res = await res.json();
      this.events = await res.events;
      this.tags = await res.tags;
      this.topEvents = await res.topEvents;
      console.log(this.events);
      console.log(this.tags);
      console.log(this.topEvents);
    },
  },
  created() {
    this.getData();
    this.$root.$refs.header.printMenu(1);
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
$card-bg: rgba(39, 39, 47, 0.4);

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
%column {
  @extend %display-start;
  flex-direction: column;
  align-items: center;
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
@mixin WH($w, $h) {
  width: $w;
  height: $h;
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
%radiobox {
  @include WH(180px, 50px);
  @extend %display-between;
  flex-direction: column;
  margin-left: 30px;
  border: none;
  label {
    @extend %display-between;
    align-items: center;
    margin: 0px 0px 10px 0px;
  }
  .radio input {
    position: absolute;
    z-index: -1;
    opacity: 0;
  }
  .radio-text {
    position: relative;
    padding: 0 0 0 35px;
    cursor: pointer;
  }
  .radio-text:before {
    content: "";
    position: absolute;
    top: 2px;
    left: 0;
    width: 20px;
    height: 20px;
    background-image: url("../assets/ProjectsEventsPage/Radio.svg");
  }
  .radio-text:after {
    content: "";
    position: absolute;
    top: 5px;
    left: 3px;
    width: 14px;
    height: 14px;
    border-radius: 50%;
    background: $text-color;
    opacity: 0;
    transition: 0.3s;
  }
  .radio input:checked + .radio-text:after {
    opacity: 1;
  }
}

.events-page {
  // width: 1900px;
  @extend %display-center;
  margin-top: 150px;
  .content {
    width: 1782px;
    height: 2430px;
    @extend %display-between;
    .best-events {
      @include WH(300px, 570px);
      @extend %column;
      position: relative;
      border-radius: 20px;
      backdrop-filter: blur(15px);
      background: $color-card;
      .card-img {
        position: absolute;
        z-index: -1;
      }
    }
    .events {
      @include WH(1051px, 2208px);
      @extend %column;
      .search {
        @include WH(920px, 80px);
        .searchline {
          height: 38px;
          @extend %display-start;
          align-items: center;
          border: 2px solid $text-color;
          background: $card-bg;
          border-radius: 25px;
          input {
            @include WH(845px, 38px);
            outline: none;
            margin-left: 12px;
            padding-left: 12px;
            background: none;
            border: none;
            border-left: 2px solid $text-color;
            color: $text-color;
          }
          img {
            margin-left: 18px;
          }
        }
        p {
          margin: 6px 0px 10px 0px;
          text-align: center;
        }
      }
    }
    .filters {
      @include WH(342px, 717px);
      @extend %column;
      position: relative;
      .menu-filters {
        @include WH(330px, 80px);
        @extend %display-between;
        align-items: center;
        border: 2px solid $text-color;
        border-radius: 10px;
        background: $bg;
        cursor: pointer;
        z-index: 1;
        p {
          margin: 0px 0px 0px 35px;
          font-size: 24px;
        }
        .arrow-filters {
          @include WH(40px, 20px);
          margin-right: 30px;
          border: none;
          background-image: url("../assets/ProjectsEventsPage/Arrow.svg");
          transition: all 0.5s ease 0s;
        }
      }
      .block-filters {
        @include WH(300px, 650px);
        margin-top: 20px;
        border-radius: 10px;
        background-color: $card-bg;
        backdrop-filter: blur(20px);
        background-image: url("../assets/ProjectsEventsPage/FiltersBackground.png");
        border: 2px solid $text-color;
        position: absolute;
        visibility: hidden;
        opacity: 0;
        z-index: -1;
        transition: all 1s ease 0s;
        .title {
          @include WH(300px, 45px);
          @extend %display-center;
          align-items: center;
          margin-bottom: 30px;
          border: none;
          p {
            font-size: 18px;
          }
        }
        .tags {
          margin-top: 10px;
          position: relative;
          .title {
            @include WH(300px, 45px);
            @extend %display-center;
            align-items: center;
            @extend %p0;
            border: none;
            position: relative;
            margin: 0;
            img {
              position: absolute;
              left: 97px;
            }
            p {
              margin-left: 10px;
            }
          }
          .searchline {
            margin-left: 20px;
            align-self: center;
            input {
              @include WH(240px, 39px);
              outline: none;
              padding-left: 20px;
              border-radius: 25px;
              border: 2px solid $text-color;
              background: $card-bg;
              color: $text-color;
            }
          }
          #ul-tags-hint {
            width: 240px;
            height: 250px;
            position: absolute;
            left: 30px;
            margin: 0;
            padding: 0;
            list-style-type: none;
            overflow: auto;
            color: $bg;
            border-radius: 10px;
            background: $text-color;
            opacity: 0;
            visibility: hidden;
            z-index: 2;
            li {
              height: 30px;
              display: list-item;
              // border: 1px solid #492;
              background: $text-color;
              padding-left: 10px;
              cursor: pointer;
              transition: 0.3s;
              &:hover {
                padding-left: 20px;
                transition: 0.3s;
                background: $main-color;
                color: $text-color;
              }
            }
            &::-webkit-scrollbar {
              width: 10px;
              height: 98%;
              border-radius: 0px 25px 25px 0px;
              background: $text-color;
            }
            &::-webkit-scrollbar-thumb {
              background: -webkit-linear-gradient(
                319.66deg,
                #20e4ff 19%,
                #8b30ff 75.54%
              );
              box-shadow: inset 2px 1000px 2px $bg;
              border: solid 2px transparent;
              background-origin: border-box;
              border-radius: 25px;
            }
          }
          .container-tags {
            @include WH(200px, 110px);
            margin: 20px 50px 0px 50px;
            display: flex;
            ul {
              width: 100px;
              height: 200px;
              display: flex;
              flex-direction: column;
              align-items: center;
              padding: 0;
              list-style-type: none;
              // border: 1px solid $text-color;
              li {
                height: 40px;
                &:hover {
                  text-decoration: line-through;
                  cursor: pointer;
                }
              }
            }
          }
        }
        .rating {
          @include WH(300px, 125px);
          margin-top: 120px;
          .title {
            margin: 0;
          }
          .order {
            @extend %radiobox;
          }
          .button-get-filters {
            font-size: 18px;
            margin: 60px 40px 0px 40px;
          }
        }
      }
    }
  }

  .arrow-rotate {
    transition: all 0.5s ease 0s;
    transform: rotate(180deg);
  }
  .block-filter-visibal {
    opacity: 1 !important;
    visibility: visible !important;
    transition: all 1s ease 0s;
    transform: translate(0px, 113px);
    z-index: 0 !important;
  }
  .visible {
    opacity: 1 !important;
    visibility: visible !important;
  }
  .hidden {
    display: none !important;
  }
  .graphics {
    position: absolute;
    z-index: -1;
    top: 1240px;
    left: 1283px;
  }
}
</style>
