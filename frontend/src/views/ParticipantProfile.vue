<template>
  <div class="profile-participant">
    <img
      src="../assets/ParticipantProfile/Graphics.png"
      alt="Graphics"
      class="graphics"
    />
    <img
      v-if="canEdit"
      src="../assets/ParticipantProfile/Edit.svg"
      alt="Edit"
      class="edit"
    />
    <img
      v-if="canEdit"
      src="../assets/ParticipantProfile/EditBcg.svg"
      alt="Edit"
      class="editBcg"
      id="edit-main"
      @click="editFunction"
    />
    <div class="content">
      <div class="left-part part-half">
        <div class="participant-card">
          <img
            src="../assets/ParticipantProfile/CardMain.png"
            alt=""
            class="card-img"
          />
          <img
            src="../assets/ParticipantProfile/EditItem.svg"
            alt="Edit"
            class="edit-item"
          />
          <img
            src="../assets/ParticipantProfile/EditItemBcg.svg"
            alt="Edit"
            class="editBcg-item"
            @click="showPopupMainCard"
          />
          <div class="participant">
            <div class="main">
              <div class="main-left">
                <img
                  v-if="user.developer"
                  :src="user.developer.photo"
                  class="participant-image"
                />
                <div class="actions"></div>
              </div>
              <div class="participant-info">
                <div class="text">
                  <div class="text-line up-line">
                    <p v-if="user.developer">
                      {{ user.developer.name + " " + user.developer.surname }}
                    </p>
                    <p v-if="user.developer">{{ user.developer.nickname }}</p>
                  </div>
                  <div class="text-line bottom-line">
                    <p v-if="user.developer">
                      Возраст: {{ user.developer.age }}
                    </p>
                    <p v-if="user.developer">Рейтинг: {{ user.rating }}</p>
                  </div>
                </div>
                <div class="tags" v-if="user.developer">
                  <img src="../assets/ParticipantProfile/CardTags.png" alt="" />
                  <div class="tags-line tags-first-line">
                    <p v-for="tag in tagsFirstLine" :key="tag"></p>
                  </div>
                  <div class="tags-line tags-second-line">
                    <p v-for="tag in tagsSecondLine" :key="tag"></p>
                  </div>
                </div>
              </div>
            </div>
            <div class="useful-links">
              <p class="title">Полезные ссылки</p>
              <div class="links-image">
                <div class="line links-useful-first-line"></div>
                <div class="line links-useful-second-line"></div>
              </div>
              <div class="links-text">
                <a><p class="text-gradient"></p></a>
                <a><p class="text-gradient"></p></a>
              </div>
            </div>
          </div>
        </div>
        <div class="info-card">
          <img
            src="../assets/ParticipantProfile/CardAbout.png"
            alt=""
            class="card-img"
          />
          <img
            src="../assets/ParticipantProfile/EditItem.svg"
            alt="Edit"
            class="edit-item"
          />
          <img
            src="../assets/ParticipantProfile/EditItemBcg.svg"
            alt="Edit"
            class="editBcg-item"
            @click="showPopupAboutCard"
          />
          <p class="title">О себе</p>
          <p id="text-about" class="text" v-if="user.developer">
            {{ user.developer.about }}
          </p>
        </div>
      </div>
      <div class="right-part part-half">
        <div class="achievements">
          <p class="title">Достижения</p>
          <div class="achievement-item"></div>
          <div class="achievement-item"></div>
          <div class="achievement-item"></div>
        </div>
        <div class="projects">
          <p class="title">Проекты</p>
          <ul id="nav">
            <li
              class="slider-text text-gradient"
              @click="swapListProjects(0, 1)"
            >
              <p class="">От ивентов</p>
            </li>
            <li class="slider-text" @click="swapListProjects(1, 0)">
              <p>Самостоятельные</p>
            </li>
            <div id="slider"></div>
          </ul>
          <ListProjects
            v-if="user.developer"
            :cards="user.projectsEvent"
            :statistics="true"
            :idList="0"
            class="list-projects list-projects-active"
          />
          <ListProjects
            v-if="user.developer"
            :cards="user.projectsNoEvent"
            :statistics="true"
            :idList="1"
            class="list-projects list-projects-hidden"
          />
        </div>
        <div class="links">
          <img
            src="../assets/ParticipantProfile/CardLinks.png"
            alt=""
            class="card-img"
          />
          <img
            src="../assets/ParticipantProfile/EditItem.svg"
            alt="Edit"
            class="edit-item"
          />
          <img
            src="../assets/ParticipantProfile/EditItemBcg.svg"
            alt="Edit"
            class="editBcg-item"
            @click="showPopupContactsLinksCard"
          />
          <p class="title">Дополнительная информация</p>
          <ul id="text-contact-links">
            <li><p></p></li>
          </ul>
          <div id="image-contact-links" class="links-image"></div>
        </div>
      </div>
    </div>
    <div id="popup-participant-card" class="popup">
      <div class="popup-body">
        <div class="title">
          <p>Редактирование полезных ссылок</p>
          <img
            src="../assets/ParticipantProfile/PopupExit.png"
            alt="exit"
            class="exit"
            @click="exitPopup"
          />
        </div>
        <div class="content">
          <div class="photo">
            <p>Загрузить фото:</p>
            <input id="input-file" type="file" />
          </div>
          <div class="useful-links">
            <p>Отредактируйте общие ссылки:</p>
            <div
              class="line-useful-links-popup links-useful-popup-first-line"
            ></div>
            <div
              class="line-useful-links-popup links-useful-popup-second-line"
            ></div>
            <div class="add" @click="openPopupUsefulLink"></div>
          </div>
          <div class="owner-links">
            <p>Отредактируйте собственные ссылки:</p>
            <div class="owner-links-line owner-links-first-line">
              <label>
                <input type="text" placeholder="Название своего сайта"
              /></label>
              <div class="minus" @click="deleteUsefulTextLink($event, 0)"></div>
              <label>
                <input type="text" placeholder="URL своего сайта"
              /></label>
            </div>
            <div class="owner-links-line owner-links-second-line">
              <label>
                <input type="text" placeholder="Название своего приложения"
              /></label>
              <div class="minus" @click="deleteUsefulTextLink($event, 1)"></div>
              <label>
                <input type="text" placeholder="URL своего приложения"
              /></label>
            </div>
          </div>
          <div class="tags-edit">
            <p>Отредактируйте теги:</p>
            <div class="tags-edit-body">
              <div class="tags-edit-line tags-edit-first-line">
                <p v-for="tag in tagsFirstLine" :key="tag"></p>
              </div>
              <div class="tags-edit-line tags-edit-second-line">
                <p v-for="tag in tagsSecondLine" :key="tag"></p>
              </div>
            </div>
            <div class="add" @click="openPopupTag"></div>
          </div>
          <div id="add-popup-useful-link" class="add-popup">
            <img
              src="../assets/ParticipantProfile/PopupExit.png"
              alt="exit"
              class="exit"
              @click="exitAddPopup"
            />
            <label>
              <input
                id="inputAddImageLink"
                type="text"
                placeholder="URL новой ссылки"
            /></label>
            <Button
              @click="addImageLink"
              class="button"
              :width="'100px'"
              :height="'20px'"
              :textButton="'Добавить'"
            />
          </div>
          <div id="add-popup-tag" class="add-popup">
            <img
              src="../assets/ParticipantProfile/PopupExit.png"
              alt="exit"
              class="exit"
              @click="exitAddPopup"
            />
            <label class="searchline">
              <input
                type="text"
                v-if="tags.length"
                placeholder="Найти тег"
                v-on:keyup="findPopupTag($event)"
                @click="getPopupListTags"
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
          </div>
          <ButtonForm
            @click="addAllChanges"
            class="button-popup-participant-card"
            :width="'400px'"
            :height="'50px'"
            :textButton="'Применить изменения'"
          />
        </div>
      </div>
    </div>
    <div id="popup-about" class="popup">
      <div class="popup-body">
        <div class="title">
          <p>Редактирование информации о себе</p>
          <img
            src="../assets/ParticipantProfile/PopupExit.png"
            alt="exit"
            class="exit"
            @click="exitPopup"
          />
        </div>
        <div class="content">
          <div class="popup-about-area">
            <p>Отредактируйте информацию о себе:</p>
            <label>
              <textarea id="popup-text-about" cols="1" rows="1"></textarea>
            </label>
          </div>
          <ButtonForm
            @click="addAboutText"
            class="button-popup-participant-card"
            :width="'400px'"
            :height="'50px'"
            :textButton="'Применить изменения'"
          />
        </div>
      </div>
    </div>
    <div id="popup-contact-links" class="popup">
      <div class="popup-body">
        <div class="title">
          <p>Редактирование контактных ссылок</p>
          <img
            src="../assets/ParticipantProfile/PopupExit.png"
            alt="exit"
            class="exit"
            @click="exitPopup"
          />
        </div>
        <div class="content content-text-contact-links">
          <div class="text-contact-links">
            <p>Отредактируйте текстовые контактные ссылки:</p>
            <div class="contact-links-line">
              <label> <input type="text" placeholder="Почта" /></label>
              <div class="minus" @click="deleteContactLink($event, 0)"></div>
            </div>
          </div>
          <div class="image-contact-links">
            <p>Отредактируйте ссылочные контактные ссылки:</p>
            <div class="contact-links-line">
              <label> <input type="text" placeholder="URL ссылки" /></label>
              <div class="minus" @click="deleteContactLink($event, 1)"></div>
            </div>
            <div class="contact-links-line">
              <label> <input type="text" placeholder="URL ссылки" /></label>
              <div class="minus" @click="deleteContactLink($event, 2)"></div>
            </div>
            <div class="contact-links-line">
              <label> <input type="text" placeholder="URL ссылки" /></label>
              <div class="minus" @click="deleteContactLink($event, 3)"></div>
            </div>
            <div class="contact-links-line">
              <label> <input type="text" placeholder="URL ссылки" /></label>
              <div class="minus" @click="deleteContactLink($event, 4)"></div>
            </div>
          </div>
          <ButtonForm
            @click="addContactLinks"
            class="button-popup-participant-card"
            :width="'400px'"
            :height="'50px'"
            :textButton="'Применить изменения'"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ListProjects from "@/components/ProfileListProjectEvent.vue";
import Button from "@/components/Buttons/ButtonAddPopup.vue";
import ButtonForm from "@/components/Buttons/ButtonProject.vue";
import $ from "jquery";
import { getDataUser } from "../api/participantProfile.js";
import { getAllTags } from "../api/participantProfile.js";
import { deleteLink } from "../api/participantProfile.js";
import { addLink } from "../api/participantProfile.js";
import { deleteTag } from "../api/participantProfile.js";
import { addTag } from "../api/participantProfile.js";
import { addAbout } from "../api/participantProfile.js";
import { addPhoto } from "../api/participantProfile.js";

let getTags = true;
let getUsefulLinks = true;
let getAchievements = true;

const toBase64 = (file) =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
  });

export default {
  data() {
    return {
      user: Object,
      nickname: String,
      tags: Object,
      tagsFirstLine: Array,
      tagsSecondLine: Array,
      idStatistics: Number,
      canEdit: false,
    };
  },
  components: {
    ListProjects,
    Button,
    ButtonForm,
  },
  props: {},
  methods: {
    swapListProjects(firstIndex, secondIndex) {
      let listsProjects = document.getElementsByClassName("list-projects");
      if (!listsProjects[firstIndex].classList.contains("list-projects-active"))
        listsProjects[firstIndex].classList.add("list-projects-active");
      if (listsProjects[firstIndex].classList.contains("list-projects-hidden"))
        listsProjects[firstIndex].classList.remove("list-projects-hidden");
      if (
        !listsProjects[secondIndex].classList.contains("list-projects-hidden")
      )
        listsProjects[secondIndex].classList.add("list-projects-hidden");
      if (listsProjects[secondIndex].classList.contains("list-projects-active"))
        listsProjects[secondIndex].classList.remove("list-projects-active");
      //Градиентный текст
      let textSlider = document.getElementsByClassName("slider-text");
      if (!textSlider[firstIndex].classList.contains("text-gradient"))
        textSlider[firstIndex].classList.add("text-gradient");
      if (textSlider[secondIndex].classList.contains("text-gradient"))
        textSlider[secondIndex].classList.remove("text-gradient");
    },
    editFunction() {
      //Разукрасить кнопку
      let edit = document.getElementById("edit-main");
      if (!edit.classList.contains("edit-after"))
        edit.classList.add("edit-after");
      else edit.classList.remove("edit-after");

      let editItems = document.getElementsByClassName("edit-item");
      let editBcgItems = document.getElementsByClassName("editBcg-item");
      for (let i = 0; i < editItems.length; i++) {
        if (!editItems[i].classList.contains("edit-item-active"))
          editItems[i].classList.add("edit-item-active");
        else editItems[i].classList.remove("edit-item-active");
        if (!editBcgItems[i].classList.contains("editBcg-item-active"))
          editBcgItems[i].classList.add("editBcg-item-active");
        else editBcgItems[i].classList.remove("editBcg-item-active");
      }
    },
    async showPopupMainCard() {
      if (getTags) {
        this.tags = await getAllTags();
        this.tags = await this.tags.json();
        getTags = false;
      }
      let popup = document.getElementById("popup-participant-card");
      if (!popup.classList.contains("popup-active"))
        popup.classList.add("popup-active");
      this.getPopupUsefulLinks();
      this.getDataTags("-edit");
    },
    showPopupAboutCard() {
      let popup = document.getElementById("popup-about");
      if (!popup.classList.contains("popup-active"))
        popup.classList.add("popup-active");
      this.getPopupAbout();
    },
    showPopupContactsLinksCard() {
      let popup = document.getElementById("popup-contact-links");
      if (!popup.classList.contains("popup-active"))
        popup.classList.add("popup-active");
      this.getPopupContactsLinks();
    },
    getPopupUsefulLinks() {
      let firstLine = document.getElementsByClassName(
        "links-useful-popup-first-line"
      )[0];
      if (firstLine.children.length === 0) {
        let secondLine = document.getElementsByClassName(
          "links-useful-popup-second-line"
        )[0];
        let textLinksHTML = document
          .getElementsByClassName("links-text")[0]
          .querySelectorAll("a");
        let inputTextLinks = document.getElementsByClassName(
          "owner-links-line"
        );
        if (this.user.usefulLinks) {
          let countFirstLine = 0;
          for (let i = 0; i < this.user.usefulLinks.length; i++) {
            if (!this.user.usefulLinks[i].name) {
              let link = document.createElement("div");
              link.className = this.getClassForUsefulLink(
                this.user.usefulLinks[i].url
              );
              link.addEventListener("click", this.deleteUsefulLink);
              link.click.bind("this");
              link.id = `i${i}`;
              if (countFirstLine < 5) firstLine.append(link);
              else secondLine.append(link);
              ++countFirstLine;
            } else {
              let index;
              if (!inputTextLinks[0].querySelector("input").value) index = 0;
              else index = 1;
              inputTextLinks[index].id = `i${i}`;
              let inputs = inputTextLinks[index].querySelectorAll("input");
              inputs[0].value = textLinksHTML[index].querySelector(
                "p"
              ).innerHTML;
              inputs[1].value = textLinksHTML[index].href;
            }
          }
        }
      }
    },
    getPopupListTags() {
      let ul = document.getElementById("ul-tags-hint");
      if (!ul.classList.contains("visible")) ul.classList.add("visible");
      else ul.classList.remove("visible");
    },
    getPopupAbout() {
      let textAbout = document.getElementById("text-about");
      let popupTextAbout = document.getElementById("popup-text-about");
      popupTextAbout.value = textAbout.innerText;
    },
    getPopupContactsLinks() {
      let inputs = document
        .getElementsByClassName("content-text-contact-links")[0]
        .querySelectorAll("input");
      let countImageLink = 1;
      for (let link in this.user.contactLinks) {
        if (this.user.contactLinks[link].url) {
          inputs[countImageLink].value = this.user.contactLinks[link].url;
          countImageLink++;
        } else {
          inputs[0].value = this.user.contactLinks[link].name;
        }
      }
    },
    findPopupTag(event) {
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
    deleteUsefulLink(th) {
      const idDelete = +th.target.id.substr(1);
      let firstLine = document.getElementsByClassName(
        "links-useful-popup-first-line"
      )[0];
      let secondLine = document.getElementsByClassName(
        "links-useful-popup-second-line"
      )[0];
      if (idDelete < 5) {
        firstLine.querySelector(`#${th.target.id}`).remove();
      } else {
        secondLine.querySelector(`#${th.target.id}`).remove();
      }
      deleteLink(
        this.token,
        this.user.usefulLinks[idDelete].type,
        this.user.usefulLinks[idDelete].url,
        this.user.usefulLinks[idDelete].name
      );
    },
    deleteUsefulTextLink(th, id) {
      let link = document.getElementsByClassName("owner-links-line")[id];
      let inputs = link.querySelectorAll("input");
      const idDelete = +link.id.substr(1);
      deleteLink(
        this.token,
        this.user.usefulLinks[idDelete].type,
        this.user.usefulLinks[idDelete].url,
        this.user.usefulLinks[idDelete].name
      );
      inputs[0].value = "";
      inputs[1].value = "";
    },
    deleteTag(th) {
      const idTag = +th.target.id.substr(1);
      let idDelete;
      for (let tag in this.user.tags)
        if (this.user.tags[tag].id === idTag) idDelete = tag;
      deleteTag(
        this.token,
        this.user.tags[idDelete].id,
        this.user.tags[idDelete].name,
        this.user.tags[idDelete].color
      );
      document
        .getElementsByClassName("tags-edit-body")[0]
        .querySelector(`#${th.target.id}`)
        .remove();
    },
    deleteContactLink(th, idInput) {
      let input = document
        .getElementsByClassName("content-text-contact-links")[0]
        .querySelectorAll("input")[idInput];
      console.log(input.value);
      deleteLink(this.token, 0, null, input.value);
      input.value = "";
    },
    openPopupUsefulLink() {
      let popup = document.getElementById("add-popup-useful-link");
      if (!popup.classList.contains("popup-active"))
        popup.classList.add("popup-active");
    },
    openPopupTag() {
      let popup = document.getElementById("add-popup-tag");
      if (!popup.classList.contains("popup-active"))
        popup.classList.add("popup-active");
    },
    addImageLink(th) {
      const input = document.getElementById("inputAddImageLink");
      let firstLine = document.getElementsByClassName(
        "links-useful-popup-first-line"
      )[0];
      let secondLine = document.getElementsByClassName(
        "links-useful-popup-second-line"
      )[0];

      let link = document.createElement("div");
      link.className = this.getClassForUsefulLink(input.value);
      link.addEventListener("click", this.deleteUsefulLink);
      link.click.bind("this");
      link.id = `i${firstLine.children.length + secondLine.children.length}`;
      if (firstLine.children.length < 5) firstLine.append(link);
      else secondLine.append(link);

      this.user.usefulLinks[this.user.usefulLinks.length] = {
        type: 1,
        url: input.value,
        name: null,
      };
      addLink(
        this.token,
        this.user.usefulLinks[this.user.usefulLinks.length - 1].type,
        this.user.usefulLinks[this.user.usefulLinks.length - 1].url,
        this.user.usefulLinks[this.user.usefulLinks.length - 1].name
      );
    },
    addTag(tag) {
      let ulFirst = document.getElementsByClassName("tags-edit-line")[0];
      let ulSecond = document.getElementsByClassName("tags-edit-line")[1];
      let needAdd = true;

      let listLiFirst = ulFirst.querySelectorAll("p");
      let listLiSecond = ulSecond.querySelectorAll("p");
      for (let i = 0; i < listLiFirst.length; i++) {
        if (tag.name === listLiFirst[i].innerText) needAdd = false;
      }
      for (let i = 0; i < listLiSecond.length; i++) {
        if (tag.name === listLiSecond[i].innerText) needAdd = false;
      }
      if (
        ulFirst.getElementsByTagName("p").length +
          ulSecond.getElementsByTagName("p").length ===
        10
      )
        needAdd = false;

      if (needAdd) {
        //Сначала добавить локально
        let newTag = document.createElement("p");
        newTag.innerHTML = tag.name;
        newTag.style.color = tag.color;
        newTag.addEventListener("click", this.deleteTag);
        newTag.click.bind("this");
        newTag.id = `i${tag.id}`;
        if (ulFirst.getElementsByTagName("p").length < 5)
          ulFirst.append(newTag);
        else if (ulSecond.getElementsByTagName("p").length < 5)
          ulSecond.append(newTag);
        addTag(this.token, tag.id, tag.name, tag.color);
      }
    },
    addAboutText() {
      let textAbout = document.getElementById("text-about");
      let popupTextAbout = document.getElementById("popup-text-about");
      console.log(popupTextAbout.value);
      textAbout.innerText = popupTextAbout.value;
      this.user.developer.about = popupTextAbout.value;
      this.exitPopup();
      addAbout(this.token, popupTextAbout.value);
    },
    async addAllChanges() {
      const links = document.getElementsByClassName("owner-links-line");
      let textLinks = new Array();
      for (let textLink in this.user.usefulLinks)
        if (this.user.usefulLinks[textLink].name)
          textLinks.push(this.user.usefulLinks[textLink]);

      for (let i = 0; i < links.length; i++) {
        const inputs = links[i].querySelectorAll("input");
        if (
          inputs[0].value != textLinks[i].name ||
          inputs[1].value != textLinks[i].url
        ) {
          deleteLink(
            this.token,
            textLinks[i].type,
            textLinks[i].url,
            textLinks[i].name
          );
          addLink(this.token, 1, inputs[1].value, inputs[0].value);
        }
      }

      const file = document.getElementById("input-file");
      const image = await toBase64(file.files[0]).catch((e) => Error(e));
      let photo = document.getElementsByClassName("participant-image")[0];
      photo.src = image;
      addPhoto(this.token, image);
      this.$router.go();
    },
    async addContactLinks() {
      let inputs = document
        .getElementsByClassName("content-text-contact-links")[0]
        .querySelectorAll("input");
      for (let link in this.user.contactLinks)
        await deleteLink(
          this.token,
          0,
          this.user.contactLinks[link].url,
          this.user.contactLinks[link].name
        );
      await addLink(this.token, 0, null, inputs[0].value);
      for (let i = 1; i < inputs.length; i++) {
        await addLink(this.token, 0, inputs[i].value, null);
      }
      this.$router.go();
    },
    exitAddPopup() {
      let popup = document.getElementsByClassName("add-popup");
      for (let i = 0; i < popup.length; i++)
        if (popup[i].classList.contains("popup-active"))
          popup[i].classList.remove("popup-active");
    },
    exitPopup() {
      let popup = document.getElementsByClassName("popup");
      for (let i = 0; i < popup.length; i++)
        if (popup[i].classList.contains("popup-active"))
          popup[i].classList.remove("popup-active");
    },
    async getData() {
      this.nickname = this.$route.params.nickname;
      if (this.userCookies) {
        if (this.nickname === this.userCookies.user.url) this.canEdit = true;
      }
      const res = await getDataUser(this.nickname);
      this.user = await res.json();
      console.log(this.user);
      if (this.user.tags.length > 5) {
        this.tagsFirstLine = this.user.tags.slice(0, 5);
        this.tagsSecondLine = this.user.tags.slice(5, this.user.tags.length);
      } else {
        this.tagsFirstLine = this.user.tags;
      }
    },
    async getDataTags(strEdit) {
      let tagsFirstLine = await document.getElementsByClassName(
        `tags${strEdit}-first-line`
      );
      let tagsSecondLine = await document.getElementsByClassName(
        `tags${strEdit}-second-line`
      );
      tagsFirstLine = tagsFirstLine[0].children;
      tagsSecondLine = tagsSecondLine[0].children;

      if (this.user.tags.length > 5) {
        for (let i = 0; i < 5; i++) {
          tagsFirstLine[i].innerHTML = this.user.tags[i].name;
          tagsFirstLine[i].style.color = this.user.tags[i].color;
          if (strEdit) {
            tagsFirstLine[i].addEventListener("click", this.deleteTag);
            tagsFirstLine[i].id = `i${this.user.tags[i].id}`;
          }
        }
        for (let i = 0; i < this.user.tags.length - 5; i++) {
          tagsSecondLine[i].innerHTML = this.user.tags[i + 5].name;
          tagsSecondLine[i].style.color = this.user.tags[i + 5].color;
          if (strEdit) {
            tagsSecondLine[i].addEventListener("click", this.deleteTag);
            tagsSecondLine[i].id = `i${this.user.tags[i + 5].id}`;
          }
        }
      } else {
        for (let i = 0; i < this.user.tags.length; i++) {
          tagsFirstLine[i].innerHTML = this.user.tags[i].name;
          tagsFirstLine[i].style.color = this.user.tags[i].color;
          if (strEdit) {
            tagsFirstLine[i].addEventListener("click", this.deleteTag);
            tagsFirstLine[i].id = `i${this.user.tags[i].id}`;
          }
        }
      }
    },
    async getDataUsefulLinks() {
      if (this.user.usefulLinks && getUsefulLinks) {
        let countFirstLine = 0;
        let linksHTML = document
          .getElementsByClassName("links-text")[0]
          .querySelectorAll("a");
        for (let i = 0; i < this.user.usefulLinks.length; i++) {
          if (!this.user.usefulLinks[i].name) {
            let a = document.createElement("a");
            a.href = this.user.usefulLinks[i].url;
            a.className = this.getClassForUsefulLink(
              this.user.usefulLinks[i].url
            );
            a.target = "_blank";
            if (countFirstLine < 5) $(".links-useful-first-line").append(a);
            else $(".links-useful-second-line").append(a);

            ++countFirstLine;
          } else {
            if (!linksHTML[0].href) {
              linksHTML[0].href = this.user.usefulLinks[i].url;
              linksHTML[0].querySelector("p").innerHTML = this.user.usefulLinks[
                i
              ].name;
            } else {
              linksHTML[1].href = this.user.usefulLinks[i].url;
              linksHTML[1].querySelector("p").innerHTML = this.user.usefulLinks[
                i
              ].name;
            }
          }
        }
        getUsefulLinks = false;
      }
    },
    getDataContactLinks() {
      if (this.user.contactLinks) {
        const contactTextLink = document
          .getElementById("text-contact-links")
          .querySelector("p");
        const contactImageLinks = document.getElementById(
          "image-contact-links"
        );

        for (let link in this.user.contactLinks) {
          if (this.user.contactLinks[link].url) {
            let a = document.createElement("a");
            a.href = this.user.contactLinks[link].url;
            a.className = this.getClassForContactLink(a.href);
            a.target = "_blank";
            contactImageLinks.append(a);
          } else {
            contactTextLink.innerHTML = this.user.contactLinks[link].name;
          }
        }
      }
    },
    getDataAchievements() {
      if (this.user.achievements && getAchievements) {
        getAchievements = false;
        this.achievements = new Array();
        for (let i = 0; i < 3; i++)
          if (this.user.achievements[i])
            this.achievements.push(this.user.achievements[i]);
        const achievementsHTML = document.getElementsByClassName(
          "achievement-item"
        );
        for (let achievement in this.achievements) {
          const a = document.createElement("a");
          a.href = "/project/" + this.achievements[achievement].projectId;
          a.innerHTML = `• ${this.achievements[achievement].projectName} - ${this.achievements[achievement].place} место`;
          achievementsHTML[achievement].append(a);
        }
      }
    },
    getClassForUsefulLink(link) {
      if (link.includes("figma")) return "img-figma";
      else if (link.includes("gitlab")) return "img-gitlab";
      else if (link.includes("github")) return "img-github";
      else if (link.includes("codeforces")) return "img-codeforces";
      else if (link.includes("habr")) return "img-habr";
      else if (link.includes("kwork")) return "img-kwork";
      else return "img-defolt";
    },
    getClassForContactLink(link) {
      if (link.includes("vk")) return "img-vk";
      else if (link.includes("twitter")) return "img-twitter";
      else if (link.includes("t.me")) return "img-telegram";
      else if (link.includes("facebook")) return "img-facebook";
      else return "img-defolt";
    },
  },
  async created() {
    this.userCookies = $cookies.get("user");
    if (this.userCookies)
      this.token = "Bearer " + this.userCookies.access_token;
    await this.getData();
    this.getDataTags("");
    this.getDataAchievements();
    await this.getDataUsefulLinks();
    this.getDataContactLinks();
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
$card-bg: rgba(39, 39, 47, 0.4);
$text-color: #f8f8ff;
$delete-color: #d80404;

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
@mixin WH($w, $h) {
  width: $w;
  height: $h;
}

.profile-participant {
  @extend %display-center;
  margin-top: 150px;
  .content {
    @include WH(1520px, 1578px);
    @extend %display-between;
    .part-half {
      @extend %display-between;
      flex-direction: column;
      align-items: center;
    }
    .left-part {
      .participant-card {
        @include WH(700px, 691px);
        align-items: center;
        @extend %display-center;
        @include Card(700px);
        .participant {
          @include WH(670px, 579px);
          .main {
            @extend %display-between;
            margin-right: 40px;
            .main-left {
              @include WH(180px, 213px);
              @extend %display-between;
              flex-direction: column;
              align-items: flex-end;
              .participant-image {
                @include WH(150px, 150px);
                border-radius: 50px;
                // background: url("../assets/ParticipantProfile/Avatar.jpg");
              }
              .actions {
                @include WH(150px, 40px);
              }
            }
            .participant-info {
              width: 420px;
              @extend %p0;
              .text {
                @include Raleway(18px);
                .text-line {
                  @extend %display-between;
                }
                .up-line {
                  p:nth-child(1) {
                    font-size: 24px;
                  }
                }
                .bottom-line {
                  margin-top: 30px;
                }
              }
              .tags {
                height: 113px;
                margin-top: 20px;
                @extend %display-around;
                flex-direction: column;
                align-items: center;
                border-radius: 25px;
                position: relative;
                img {
                  position: absolute;
                  z-index: -1;
                }
                .tags-line {
                  width: 380px;
                  @extend %display-between;
                }
              }
            }
          }
          .useful-links {
            margin-top: 15px;
            @extend %display-center;
            flex-direction: column;
            align-items: center;
            .title {
              @include Raleway(24px);
            }
            .links-image {
              @include WH(580px, 210px);
              @extend %display-between;
              flex-direction: column;
              margin-top: 30px;
              .line {
                a {
                  @include WH(80px, 80px);
                  margin-left: 45px;
                  transition: all 300ms ease;
                  &:hover {
                    transition: all 300ms ease;
                    transform: scale(0.9);
                  }
                }
                @extend %display-start;
                a:nth-child(1) {
                  margin-left: 0px;
                }
                .img-defolt {
                  background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Defolt.png");
                }
                .img-codeforces {
                  background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Codeforce.png");
                }
                .img-figma {
                  background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Figma.png");
                }
                .img-github {
                  background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Github.png");
                }
                .img-gitlab {
                  background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Gitlab.png");
                }
                .img-kwork {
                  background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Kwork.png");
                }
                .img-habr {
                  background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Hubr.png");
                }
              }
            }
            .links-text {
              width: 580px;
              margin-top: 41px;
              @extend %display-around;
              a {
                transition: all 300ms ease;
                &:hover {
                  transition: all 300ms ease;
                  transform: scale(1.1);
                }
              }
            }
            @extend %p0;
          }
        }
      }
      .info-card {
        @include WH(700px, 857px);
        @extend %p0;
        @include Card(700px);
        .title {
          @extend %title;
        }
        .text {
          margin: 20px 50px;
          white-space: pre-wrap;
        }
      }
    }
    .right-part {
      width: 790px;
      .achievements {
        width: 100%;
        height: 269px;
        @extend %p0;
        .title {
          @extend %title;
        }
        .achievement-item {
          height: 50px;
          @extend %display-start;
          margin: 0px 35px;
          align-items: center;
        }
      }
      .projects {
        height: 949px;
        @extend %display-between;
        flex-direction: column;
        align-items: center;
        @extend %p0;
        .title {
          @extend %title;
        }
        #nav {
          @include WH(790px, 62px);
          @extend %display-around;
          padding: 0;
          position: relative;
          li {
            list-style-type: none;
            float: left;
            align-self: center;
            cursor: pointer;
            p {
              @include Raleway(24px);
            }
          }
          #slider {
            left: 0px;
            position: absolute;
            left: 197.5px;
            top: 58px;
            height: 2px;
            background: $color-icon;
            width: 395px;
            transition: all 300ms ease;
            opacity: 0;
          }
          li:nth-child(1):hover ~ #slider {
            left: 0px;
            opacity: 1;
          }
          li:nth-child(2):hover ~ #slider {
            left: 395px;
            opacity: 1;
          }
        }
      }
      .links {
        width: 100%;
        height: 300px;
        @extend %display-between;
        flex-direction: column;
        align-items: center;
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
          align-items: center;
          margin-bottom: 50px;
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
    top: 1490px;
    left: 55px;
  }
  .edit {
    position: absolute;
    left: 1730px;
    width: 150px;
    height: 150px;
    transition: all 300ms ease;
  }
  .editBcg {
    @include WH(150px, 150px);
    position: absolute;
    left: 1732px;
    opacity: 0;
    cursor: pointer;
    transition: all 300ms ease;
    &:hover {
      opacity: 1;
    }
    &:active {
      transform: scale(1.3);
    }
  }
  .edit-after {
    opacity: 1;
    transform: scale(1.3);
  }

  .edit-item {
    @include WH(100px, 100px);
    position: absolute;
    top: -40px;
    right: -50px;
    opacity: 0;
    transition: all 500ms ease;
    transform: translate(0px, -50px);
  }
  .editBcg-item {
    @include WH(100px, 100px);
    position: absolute;
    top: -39px;
    right: -50px;
    display: none;
  }
  .edit-item-active {
    opacity: 1;
    transition: all 500ms ease;
    transform: translate(0px, 0px);
  }
  .editBcg-item-active {
    display: block;
    opacity: 0;
    transition: all 300ms ease;
    cursor: pointer;
    &:hover {
      transition: all 300ms ease;
      opacity: 1;
    }
  }

  .list-projects-active {
    display: block;
  }
  .list-projects-hidden {
    display: none;
  }
  .text-gradient {
    background: $main-color;
    background-clip: text;
    -webkit-text-fill-color: transparent;
  }

  #popup-participant-card {
    width: 100%;
    height: 109.5%;
    position: absolute;
    left: 0;
    top: 0;
    display: none;
    background: rgba(6, 6, 6, 0.65);
    backdrop-filter: blur(5px);
    .popup-body {
      width: 1100px;
      height: 1550px;
      @extend %display-start;
      flex-direction: column;
      align-items: center;
      position: relative;
      margin: 300px 401px 0px 401px;
      border: solid 5px transparent;
      border-radius: 25px;
      background: $main-color;
      background-origin: border-box;
      box-shadow: inset 1px 2000px 1px rgb(20, 20, 20, 1);
      .title {
        height: 80px;
        display: flex;
        justify-content: center;
        align-items: center;
        p {
          font-size: 30px;
          font-family: Raleway;
          text-align: center;
        }
      }
      .content {
        @include WH(900px, 1520px);
        @extend %display-start;
        flex-direction: column;
        align-items: center;
        .photo {
          @include WH(500px, 50px);
          @extend %display-between;
          align-items: center;
          margin-top: 70px;
          input {
            @include WH(250px, 20px);
            cursor: pointer;
          }
        }
        .useful-links {
          display: flex;
          flex-direction: column;
          margin-top: 70px;
          height: 360px;
          .line-useful-links-popup {
            height: 80px;
            margin-top: 30px;
            position: relative;
            div {
              @include WH(80px, 80px);
              margin-left: 120px;
              cursor: pointer;
              transition: all 300ms ease;
              &:hover {
                transition: all 300ms ease;
                box-shadow: 0px 0px 1px 3px $delete-color;
                transform: scale(0.9);
              }
            }
            @extend %display-start;
            div:nth-child(1) {
              margin-left: 0px;
            }
            .img-defolt {
              background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Defolt.png");
            }
            .img-codeforces {
              background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Codeforce.png");
            }
            .img-figma {
              background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Figma.png");
            }
            .img-github {
              background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Github.png");
            }
            .img-gitlab {
              background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Gitlab.png");
            }
            .img-kwork {
              background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Kwork.png");
            }
            .img-habr {
              background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Hubr.png");
            }
          }
          .links-useful-popup-first-line {
            margin-top: 50px;
          }
        }
        .owner-links {
          width: 100%;
          display: flex;
          flex-direction: column;
          margin-top: 70px;
          .owner-links-line {
            height: 38px;
            @extend %display-between;
            align-items: center;
            margin-top: 30px;
            label {
              input {
                @include WH(400px, 39px);
                outline: none;
                font-size: 16px;
                text-align: center;
                border-radius: 5px;
                background: $text-color;
                color: $bg;
              }
            }
            .minus {
              @include WH(30px, 20px);
              background-image: url("../assets/ParticipantProfile/Minus.svg");
              cursor: pointer;
              transition: all 0.3s ease;
              &:hover {
                transition: all 0.3s ease;
                transform: scale(1.1);
              }
            }
          }
        }
        .tags-edit {
          display: flex;
          flex-direction: column;
          margin-top: 70px;
          .tags-edit-body {
            @include WH(900px, 113px);
            margin-top: 30px;
            @extend %display-between;
            flex-direction: column;
            // align-items: center;
            border: solid 2px transparent;
            border-radius: 25px;
            background: $main-color;
            background-origin: border-box;
            box-shadow: inset 1px 1000px 1px rgb(20, 20, 20, 1);
            .tags-edit-line {
              @include WH(800px, 50px);
              @extend %display-between;
              align-self: center;
              align-items: center;
              p {
                cursor: pointer;
                transition: all 300ms ease;
                &:hover {
                  transition: all 300ms ease;
                  box-shadow: 0px 0px 1px 1px $delete-color;
                  transform: scale(0.9);
                }
              }
            }
          }
        }
        .add-popup {
          @include WH(900px, 100px);
          position: absolute;
          display: none;
          border-radius: 25px;
          background: $text-color;
          img {
            @include WH(30px, 30px);
            right: 10px;
          }
          label {
            input {
              @include WH(400px, 30px);
              margin: 10px 250px;
              outline: none;
              font-size: 16px;
              text-align: center;
              border-radius: 5px;
              background: $text-color;
              color: $bg;
            }
          }
          .button {
            margin: 0px 383px;
          }
        }
        #add-popup-useful-link {
          top: 550px;
        }
        #add-popup-tag {
          top: 1120px;
          .searchline {
            input {
              @include WH(376px, 40px);
              margin-top: 30px;
              outline: none;
              padding-left: 20px;
              border-radius: 25px;
              border: 2px solid $text-color;
              background: $card-bg;
              color: $text-color;
            }
          }
          #ul-tags-hint {
            width: 300px;
            height: 250px;
            position: absolute;
            left: 300px;
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
        }
        .add {
          @include WH(60px, 60px);
          margin-top: 35px;
          align-self: center;
          background-image: url("../assets/ParticipantProfile/Add.svg");
          cursor: pointer;
          transition: all 0.3s ease;
          &:hover {
            transition: all 0.3s ease;
            transform: scale(1.1);
          }
        }
        .button-popup-participant-card {
          margin-top: 270px;
        }
      }
    }
    @extend %p0;
    p {
      font-size: 18px;
    }
  }
  #popup-about {
    width: 100%;
    height: 109.5%;
    position: absolute;
    left: 0;
    top: 0;
    display: none;
    background: rgba(6, 6, 6, 0.65);
    backdrop-filter: blur(5px);
    .popup-body {
      width: 1100px;
      height: 750px;
      @extend %display-start;
      flex-direction: column;
      align-items: center;
      position: relative;
      margin: 300px 401px 0px 401px;
      border: solid 5px transparent;
      border-radius: 25px;
      background: $main-color;
      background-origin: border-box;
      box-shadow: inset 1px 2000px 1px rgb(20, 20, 20, 1);
      .title {
        height: 80px !important;
        display: flex;
        justify-content: center;
        align-items: center;
        p {
          font-size: 30px;
          font-family: Raleway;
          text-align: center;
        }
      }
      .content {
        @include WH(900px, 670px);
        @extend %display-start;
        flex-direction: column;
        align-items: center;
        .popup-about-area {
          @include WH(900px, 360px);
          display: flex;
          flex-direction: column;
          margin-top: 70px;
          label {
            @include WH(900px, 360px);
            margin-top: 50px;
            textarea {
              @include WH(860px, 360px);
              padding: 20px;
              resize: none;
              outline: none;
              font-size: 16px;
              font-family: OpenSans;
              border-radius: 5px;
              background: $text-color;
              color: $bg;
            }
          }
        }
        .button-popup-participant-card {
          margin-top: 150px;
        }
      }
    }
    @extend %p0;
    p {
      font-size: 18px;
    }
  }
  #popup-contact-links {
    width: 100%;
    height: 109.5%;
    position: absolute;
    left: 0;
    top: 0;
    display: none;
    background: rgba(6, 6, 6, 0.65);
    backdrop-filter: blur(5px);
    .popup-body {
      @include WH(1100px, 800px);
      @extend %display-start;
      flex-direction: column;
      align-items: center;
      position: relative;
      margin: 300px 401px 0px 401px;
      border: solid 5px transparent;
      border-radius: 25px;
      background: $main-color;
      background-origin: border-box;
      box-shadow: inset 1px 2000px 1px rgb(20, 20, 20, 1);
      .title {
        height: 80px;
        display: flex;
        justify-content: center;
        align-items: center;
        p {
          font-size: 30px;
          font-family: Raleway;
          text-align: center;
        }
      }
      .content {
        @include WH(900px, 800px);
        @extend %display-start;
        flex-direction: column;
        align-items: center;
        .text-contact-links {
          @include WH(900px, 100px);
          margin-top: 70px;
        }
        .image-contact-links {
          @include WH(900px, 300px);
          margin-top: 70px;
        }
        .contact-links-line {
          height: 38px;
          @extend %display-center;
          align-items: center;
          margin-top: 30px;
          label {
            input {
              @include WH(400px, 39px);
              outline: none;
              font-size: 16px;
              text-align: center;
              border-radius: 5px;
              background: $text-color;
              color: $bg;
            }
          }
          .minus {
            @include WH(30px, 20px);
            margin-left: 20px;
            background-image: url("../assets/ParticipantProfile/Minus.svg");
            cursor: pointer;
            transition: all 0.3s ease;
            &:hover {
              transition: all 0.3s ease;
              transform: scale(1.1);
            }
          }
        }
        .button-popup-participant-card {
          margin-top: 70px;
        }
      }
    }
    @extend %p0;
    p {
      font-size: 18px;
    }
  }
  .popup {
    .exit {
      position: absolute;
      right: 30px;
      cursor: pointer;
    }
  }
  .popup-active {
    display: block !important;
  }
  .visible {
    opacity: 1 !important;
    visibility: visible !important;
  }
  .hidden {
    display: none !important;
  }

  .img-defolt {
    background-image: url("../assets/ParticipantProfile/LogoUsefulLinks/Defolt.png");
  }
  .img-vk {
    @include WH(60px, 60px);
    background-image: url("../assets/ParticipantProfile/LogoVK.svg");
  }
  .img-twitter {
    @include WH(80px, 80px);
    background-image: url("../assets/ParticipantProfile/LogoTwitter.svg");
  }
  .img-telegram {
    @include WH(60px, 60px);
    background-image: url("../assets/ParticipantProfile/LogoTelegram.svg");
  }
  .img-facebook {
    @include WH(63px, 68px);
    background-image: url("../assets/ParticipantProfile/LogoFacebook.svg");
  }
}
</style>
