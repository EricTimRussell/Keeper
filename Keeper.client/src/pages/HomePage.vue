<template>
  <main class="container-fluid">
    <div class="row">
      <div class="col-sm-6 col-md-3 my-4" v-for="k in keeps" :key="k.id">
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

</style>

