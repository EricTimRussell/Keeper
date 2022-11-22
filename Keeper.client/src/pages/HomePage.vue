<template>

  <body class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-md-10 col-12 masonry">
        <KeepCard :keeps="k" v-for="k in keeps" :key="k.id" />
      </div>
    </div>
  </body>



</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { AppState } from "../AppState";
import KeepCard from "../components/KeepCard.vue";
import { keepsService } from "../services/KeepsService";
import Pop from "../utils/Pop";

export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        Pop.error(error.message, "Getting Keeps")
      }
    }
    onMounted(() => {
      getAllKeeps()
    })
    return {
      profile: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      keep: computed(() => AppState.activeKeep),
    }
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
body {
  margin: 0;
  padding: 1rem;
}

.masonry {
  columns: 4;
  column-gap: 2rem;

  img.photo {
    width: 20vw;
    margin-top: 2rem
  }
}

@media screen and (max-width: 768px) {
  .masonry {
    columns: 2;
    margin-left: 2rem;
    margin-right: 2rem;
  }

  .margin {
    margin-left: 15rem;
    margin-right: 15rem;
  }


}
</style>

