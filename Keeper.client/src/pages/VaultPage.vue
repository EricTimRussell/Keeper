<template>
  <main class="container-fluid">
    <div class="row">
      <div class="col-12">
        Vault Image
        <i class="mdi mdi-dots-horizontal" selectable title="Vault Options"></i>
      </div>
    </div>
    <div class="row">
      <div class="col-md-8">
        <!-- Keeps here -->
      </div>
    </div>
  </main>
</template>


<script>
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { vaultsService } from "../services/VaultsService";
import Pop from "../utils/Pop";

export default {
  setup() {
    const route = useRoute({})
    async function getKeepsByVaultId() {
      try {
        await vaultsService.getKeepsByVaultId(route.params.id)
      } catch (error) {
        Pop.error(error, "Getting Vault Keeps By Id")
      }
    }
    onMounted(() => {
      getKeepsByVaultId()
    })
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>