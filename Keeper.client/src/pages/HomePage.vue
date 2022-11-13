<template>
  <main class="container-fluid">
    <div class="row margin my-5">
      <div class="col-md-3 col-sm-6 my-3" v-for="k in keeps" :key="k.id">
        <!-- Inject KeepCard -->
        <KeepCard :keeps="k" />
      </div>
    </div>
  </main>



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
.margin {
  margin-left: 15rem;
  margin-right: 15rem;
}

.bricks {
  columns: 4;

  img.photo {
    width: 192px;
    margin-top: 1.5rem
  }
}
</style>

