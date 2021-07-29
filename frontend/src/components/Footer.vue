<template>
  <div class="footer">
    <p>Qoollo</p>
  </div>
</template>

<script>
export default {
  components: {},
  props: {
    textCard: String,
  },
};
</script>

<style lang="scss">
$background: rgba(39, 39, 47, 0.4);
.footer {
  height: 100px;
  font-family: Raleway;
  display: flex;
  justify-content: flex-end;
  background: linear-gradient(
    104.24deg,
    #494949 3.31%,
    rgba(103, 103, 103, 0.1) 98.73%
  );
  p {
    padding: 20px 20px 0px 0px;
  }
}
</style>
