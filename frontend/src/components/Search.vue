<template>
  <div class="search">
    <input v-if="help" @keyup="keyUp" type="text" class="search__input" :name="name" :placeholder="placeholder">
    <input v-else type="text" class="search__input" :name="name" :placeholder="placeholder">
    <div v-if="active" @click="onclick" class="search__btn"></div>
    <div v-if="help" class="search__helper" :class="{'search__helper_visible': helping}">
      <p class="search__helper-p" v-for="(alike, ix) in alikes" :class="{'search__helper-p_hgl': (ix == highlight)}" :id="'h' + ix" @click="fill(alike)">
        {{alike.name}}
      </p>
    </div>
  </div>
</template>

<script>
export default {
  components: {
  },
  props: {
    help: Boolean,
    active: Boolean,   
    name: String,
    tags: Array,
    onclick: Function,
    hook: Function,
    placeholder: String
  },
  data: function() {
    return {
      helping: false,
      alikes: [],
      highlight: -1,
      selected: []
    }
  },
  methods: {
    keyUp(e) {
      if (e.keyCode == 40) {
        if (this.highlight < this.alikes.length - 1)
          this.highlight += 1;
      } else if (e.keyCode == 38) {
        if (this.highlight > -1)
          this.highlight -= 1;
      } else if (e.keyCode == 13) {
        let el = document.getElementsByClassName('search__helper-p_hgl')[0]
        el.click();
      } else {
        let val = document.getElementsByName(this.name)[0].value;
        let ind = val.lastIndexOf(' ');
        if (ind + 1 < val.length) {
          this.alikes = []
          this.helping = true;
          let subtag = val.slice(ind + 1).toLowerCase();
          this.tags.forEach(tag => {
            if (tag.name.toLowerCase().includes(subtag))
              this.alikes.push(tag);
          });
        } else {
          this.helping = false;
          this.highlight = -1;
        }
      }
    },
    fill(tag) {
      if (!this.selected.includes(tag)) {
        this.selected.push(tag);
        this.fill_txt(tag.name);
        if (this.hook)
          this.hook(tag);
      }
    },
    fill_txt(txt) {
      let inp = document.getElementsByName(this.name)[0];
      let val= inp.value;
      let ind = val.lastIndexOf(' ');
      let new_val = val.slice(0, ind + 1) + txt;
      inp.value = new_val + ' ';
      inp.focus();
      this.helping = false;
      this.highlight = -1;
    },
    clear() {
      let inp = document.getElementsByName(this.name)[0];
      inp.value = '';
      this.alikes.splice(0);
      inp.focus();
      this.helping = false;
      this.highlight = -1;
      this.selected.splice(0);
    }
  }
};
</script>


<style lang="scss">
$bg-color: #060606;
$main-color: linear-gradient(319.66deg, #20E4FF 19%, #8B30FF 75.54%);
$card-border: linear-gradient(90deg, rgba(73,73,73,1) 0%, rgba(0,0,0,0) 100%);
$card-bg: rgba(39, 39, 47, 0.4);

.search {
  position: relative;
  &__helper {
    z-index: 3;
    position: absolute;
    top: 33px;
    left: 9px;
    width: calc(100% - 26px);
    padding: 5px;
    text-align: left;
    background-color: $bg-color;
    border: 2px solid #f8f8ff;
    border-top: none;
    display: none;
    border-bottom-right-radius: 10px;
    border-bottom-left-radius: 10px;
    &-p {
      margin: 0;
      &:hover {
        cursor: pointer;
      }
      &_hgl {
        background: $main-color;
        background-clip: text;
        -webkit-text-fill-color: transparent;
      }
    }
    &_visible {
      display: block;
    }
  }
  &__input {
    height: 30px;
    border-radius: 15px;
    width: calc(100% - 10px);
    border: 2px solid #f8f8ff;
    color: #f8f8ff;
    padding-left: 10px;
    background: none;
    &:focus {
      outline: none;
    }
  }
  &__btn {
    position: absolute;
    right: 0;
    top: 0;
    height: 100%;
    border-left: 1px solid #f8f8ff;
    background: url('../assets/Users/magn.png');
    width: 40px;
    background-repeat: no-repeat;
    background-position: center;
    &:hover {
      cursor: pointer;
      background: url('../assets/Users/magn_clr.png');
      background-repeat: no-repeat;
      background-position: center;
    }
  }

  &__img {
    margin-top: 7px;
    margin-left: 7px;
  }
}
</style>
