<template>

</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";


export default {

  setup(props) {
    return {
      profile: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.no-pad {
  padding: 0%;
}
</style>