<template>
    <div class="bar">
      <div class="bar-section bar-section_active">
        подготовка
        <div class="bar-section__thin"><div class="bar-section__fill bar-section__fill_thin"></div></div>
        <div class="bar-section__thic"><div class="bar-section__fill bar-section__fill_thic"></div></div>
      </div>
      <div class="bar-section" :class="{'bar-section_active':state > 0}">
        проводится
        <div class="bar-section__thin"><div class="bar-section__fill bar-section__fill_thin"></div></div>
        <div class="bar-section__thic"><div class="bar-section__fill bar-section__fill_thic"></div></div>
      </div>
      <div class="bar-section" :class="{'bar-section_active':state > 1}">
        завершён
        <div class="bar-section__thin"><div class="bar-section__fill bar-section__fill_thin"></div></div>
        <div class="bar-section__thic"><div class="bar-section__fill bar-section__fill_thic"></div></div>
      </div>
  </div>
</template>

<script>
import Card from "@/components/Card.vue";

export default {
  components: {
    Card,
  },
  props: {
    state: Number,
  }
};
</script>


<style lang="scss">
$main-color: linear-gradient(319.66deg, #20E4FF 19%, #8B30FF 75.54%);
$card-border: linear-gradient(90deg, rgba(73,73,73,1) 0%, rgba(0,0,0,0) 100%);
$card-bg: rgba(39, 39, 47, 0.4);

.bar {
  display: flex;
  justify-content: space-between;
  margin-bottom: 10px;
}
.bar-section {
  position: relative;
  width: calc(100% / 3 - 10px);
  height: 43px;
  vertical-align: top;
  padding-left: 7px;
  &_active &__fill {
    background: $main-color;
  }
  &__thin, &__thic {
    display: inline-block;
    vertical-align: top;
    position: absolute;
    overflow: hidden;
  }
  &__thin {
    width: calc(100% - 25px);
    height: 14px;
    left: 0;
    top: 21px;
  }
  &__thic {
    width: 30px;
    height: 30px;
    border-radius: 50%;
    left: calc(100% - 30px);
    top: 13px;
  }
  &__fill {
    position: absolute;
    background: #424242;
    width: 320px;
    height: 30px;
    &_thin {
      right: -30px;
      top: -8px;
    }
    &_thic {
      right: 0;
      top: 0;
    }
  }
}
</style>
